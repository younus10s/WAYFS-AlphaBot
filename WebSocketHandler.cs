using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApplication {
public class WebSocketHandler
    {
        public async Task HandleWebSocketAsync(WebSocket webSocket)
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
                        string clientMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Console.WriteLine($"Received from client: {clientMessage}");


                        // Send a JSON string of doubles
                        
                        double[] data = new double[] { 1.23, 4.56, 7.89 }; // Replace with your data
                        string serializedData = JsonConvert.SerializeObject(data);
                        byte[] dataBytes = Encoding.UTF8.GetBytes(serializedData);

                        await webSocket.SendAsync(new ArraySegment<byte>(dataBytes), WebSocketMessageType.Text, true, CancellationToken.None);

                        // Send a string message back to the client

                        // string serverMessage = "This is a server message.";
                        // byte[] serverMessageBytes = Encoding.UTF8.GetBytes(serverMessage);
                        // await webSocket.SendAsync(new ArraySegment<byte>(serverMessageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket error: {ex.Message}");
            }
        }
    }
}