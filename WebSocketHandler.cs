using System.Net.WebSockets;
using System.Text;

namespace ConsoleApplication {
public class WebSocketHandler
    {
        private string clientMessage = "";
        public async Task HandleWebSocketAsync(WebSocket webSocket, AppCmdParser cmdParser)
        {
            try
            {
                var buffer = new byte[1024];

                while (webSocket.State == WebSocketState.Open)
                {
                    // Receive a message from the client
                    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        clientMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        Console.WriteLine($"Received from client: {clientMessage} \n");

                        await Task.Delay(1000); // delay task

                        cmdParser.RunCommands(clientMessage);

                        // Send a string message back to the client
                        // string serverMessage = "This is a server message.";
                        // byte[] serverMessageBytes = Encoding.UTF8.GetBytes(serverMessage);
                        // await webSocket.SendAsync(new ArraySegment<byte>(serverMessageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket exception caught: {ex.Message}");
            }
            
        }
    }
}