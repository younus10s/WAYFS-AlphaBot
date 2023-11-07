// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Net.Sockets;
// using System.Net.WebSockets;
// using System.Threading.Tasks;

// namespace ConsoleApplication 
// {
// class App {

//     public static string clientMessage = "";
//     public static WebSocketHandler webSocketHandler = new WebSocketHandler();
//     public static async Task<string> StartApp(string [] args) {
//                 var builder = WebApplication.CreateBuilder(args);
//                 var app = builder.Build();

//                 app.UseWebSockets();

//                 app.Use(async (context, next) =>
//                 {
//                     if (context.WebSockets.IsWebSocketRequest)
//                         {
//                             Task<string> clientMessage = ReceiveMessage(context);

//                             Console.WriteLine($"Message received from client: {clientMessage}");
//                         } else {
//                             await next();
//                             }
//                         });
                        
//             await app.RunAsync();

//             return clientMessage;
//             }

//     public static async Task<string> ReceiveMessage(HttpContext context)  {
//        // string clientMessage = "";

//          WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
//         string clientMessage = await webSocketHandler.HandleWebSocketAsync(webSocket);
        
//         return clientMessage;
//     }    
// }
// }