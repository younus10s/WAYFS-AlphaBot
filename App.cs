using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ConsoleApplication 
{
class App {

    public static WebSocketHandler webSocketHandler = new WebSocketHandler();
    public static async Task StartApp(string [] args) {
                var builder = WebApplication.CreateBuilder(args);
                var app = builder.Build();

                app.UseWebSockets();

                app.Use(async (context, next) =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                        {
                            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                            await webSocketHandler.HandleWebSocketAsync(webSocket);
                        } else {
                            await next();
                            }
                        });
                        
                await app.RunAsync();
}
}
}