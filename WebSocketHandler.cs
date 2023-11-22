using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ConsoleApplication {


public class MSG
{
    public string Title { get; set; } = string.Empty; // Default to an empty string
    public List<string> Msg { get; set; } = new List<string>(); // Default to an empty list
}

public class WebSocketHandler
    {
        private WebSocket WebSocket;


        public WebSocketHandler(WebSocket WebSocket_){
            WebSocket = WebSocket_;
        }


        public async Task<string> reciveMessage(){
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

        public async Task SendMessage(string msg){
            byte[] serverMessageBytes = Encoding.UTF8.GetBytes(msg);
            await WebSocket.SendAsync(new ArraySegment<byte>(serverMessageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }


        public async Task HandleWebSocketAsync(AppCmdParser cmdParser)
        {
            try
            {
                var buffer = new byte[1024];

                while (WebSocket.State == WebSocketState.Open)
                {
                        string clientMessage = await reciveMessage();
                        Console.WriteLine("Received JSON: " + clientMessage);
                        MSG? message = JsonSerializer.Deserialize<MSG>(clientMessage);

                        for(int i = 0; i < message?.Msg.Count; i++){
                            var dataToSend = new MSG
                            {
                                Title = "status",
                                Msg = new List<string> {message.Msg[i]} 
                            };

                            string sendMsg = JsonSerializer.Serialize(dataToSend);
                            await SendMessage(sendMsg);
                            Console.WriteLine($"Send: {sendMsg} \n");

                            await cmdParser.RunCommand(message.Msg[i]);
                            
                        }
                        

                        var doneMsg = new MSG
                        {
                            Title = "Done",
                            Msg = new List<string> {"Thank you"} 
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
    }
}