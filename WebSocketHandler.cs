using System.Net.WebSockets;
using System.Text;

namespace ConsoleApplication {
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

                Console.WriteLine($"Received from client: {clientMessage} \n");
                return clientMessage;
            }
            return "";
        }


        public async Task HandleWebSocketAsync(AppCmdParser? cmdParser = null)
        {
            string clientMessage = "";
            try
            {
                var buffer = new byte[1024];

                while (WebSocket.State == WebSocketState.Open)
                {
                    
                        clientMessage = await reciveMessage();
                        //Send a string message back to the client
                        // string serverMessage = "Hej Hej.";
                        // byte[] serverMessageBytes = Encoding.UTF8.GetBytes(serverMessage);
                        // await WebSocket.SendAsync(new ArraySegment<byte>(serverMessageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket exception caught: {ex.Message}");
            }
            
        }
    }
}