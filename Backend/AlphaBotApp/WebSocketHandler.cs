using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Globalization;

namespace ConsoleApplication
{
    public class MSG
    {
        public string Title { get; set; } = string.Empty;
        public List<string> Msg { get; set; } = new List<string>();
    }

    public class WebSocketHandler
    {
        private WebSocket WebSocket;

        public WebSocketHandler(WebSocket WebSocket_)
        {
            WebSocket = WebSocket_;
        }


        public async Task<string> reciveMessage()
        {
            var buffer = new byte[1024];
            string clientMessage = "";
            WebSocketReceiveResult result = await WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                clientMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                return clientMessage;
            }

            return "";
        }

        public async Task SendMessage(string msg)
        {
            byte[] serverMessageBytes = Encoding.UTF8.GetBytes(msg);
            await WebSocket.SendAsync(new ArraySegment<byte>(serverMessageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task HandleWebSocketAsyncFreeBot(FreeBot FBot = null)
        {
            try
            {
                var buffer = new byte[1024];

                while (WebSocket.State == WebSocketState.Open)
                {
                    string clientMessage = await reciveMessage();
                    Console.WriteLine("Received JSON: " + clientMessage);
                    MSG? message = JsonSerializer.Deserialize<MSG>(clientMessage);

                    if(message?.Title == "movement"){
                        FBot.Move(double.Parse(message.Msg[0], CultureInfo.InvariantCulture), double.Parse(message.Msg[1], CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket exception caught: {ex.Message}");
            }
        }

        public async Task HandleWebSocketAsync(AppCmdParser cmdParser = null)
        {
            try
            {
                var buffer = new byte[1024];

                while (WebSocket.State == WebSocketState.Open)
                {
                    string clientMessage = await reciveMessage();
                    Console.WriteLine("Received JSON: " + clientMessage);
                    MSG? message = JsonSerializer.Deserialize<MSG>(clientMessage);
                    List<string>? actions;

                    Console.WriteLine(message?.Title);
                    if (message?.Title == "shutdown")
                    {
                        Console.WriteLine("Shutdowm");
                        break;
                    }

                    if(message?.Title == "placing"){
                        await cmdParser.RunCommand(message.Msg[0]);
                        Console.WriteLine("Placing" + cmdParser.Gunnar.PosX + " " + cmdParser.Gunnar.PosY + " " +  cmdParser.Gunnar.Heading);
                        continue;
                    }
                    if(message?.Title == "gridCoor"){
                        Console.WriteLine(cmdParser.Gunnar.PosX + ":" + cmdParser.Gunnar.PosY + ":" + cmdParser.Gunnar.Heading);
                        actions = cmdParser.Gunnar.FindPath(cmdParser.Gunnar.PosX, cmdParser.Gunnar.PosY, cmdParser.Gunnar.Heading, int.Parse(message.Msg[0]), int.Parse(message.Msg[1]) );
                        Console.Write("Actions: (");
                        foreach(var action in actions){
                            Console.Write(action + " ");
                        }
                        Console.WriteLine(")");
                    }
                    else if(message?.Title == "command")
                        actions = message?.Msg;
                    else
                        actions = new();

                    await ProcessMessageAsync(actions, cmdParser);

                    var doneMsg = new MSG
                    {
                        Title = "Done",
                        Msg = new List<string> { "Thank you" }
                    };

                    string done = JsonSerializer.Serialize(doneMsg);
                    await SendMessage(done);
                    Console.WriteLine($"Send: {done} \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket exception caught: {ex.Message}");
            }
        }

        private async Task ProcessMessageAsync(List<string>? actions, AppCmdParser cmdParser)
        {
            if (cmdParser != null && actions != null)
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    var dataToSend = new MSG
                    {
                        Title = "status",
                        Msg = new List<string> {i.ToString(), actions[i], cmdParser.Gunnar.PosX.ToString(), cmdParser.Gunnar.PosY.ToString(), cmdParser.Gunnar.Heading}
                    };

                    string sendMsg = JsonSerializer.Serialize(dataToSend);
                    await SendMessage(sendMsg);
                    Console.WriteLine($"Send: {sendMsg} \n");
                    Thread.Sleep(100);
                    await cmdParser.RunCommand(actions[i]);
                }
            }
        }
    }
}