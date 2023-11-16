using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ConsoleApplication
{
class Program
{

    public static string clientMessage = "";
    public static WebSocketHandler webSocketHandler = new WebSocketHandler();

        static async Task Main(string[] args)
        {
            GridBot Gunnar = new GridBot(5, 5);
            // TxtParser TParser = new TxtParser(Gunnar);
            // TParser.RunFile("robot.txt");

            AppCmdParser cmdParser = new AppCmdParser(Gunnar);
            
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseWebSockets();

            app.Use(async (context, next) =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                        {
                            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                            await webSocketHandler.HandleWebSocketAsync(webSocket, cmdParser);


                            
                        } else {
                                await next();
                        }
                });
        
            await app.RunAsync();


            }
}

}