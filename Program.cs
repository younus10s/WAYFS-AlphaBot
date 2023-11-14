using System.Net.WebSockets;
using System.Text;

namespace ConsoleApplication
{
class Program
{

    public static string clientMessage = "";
    public static WebSocketHandler webSocketHandler = new WebSocketHandler();

    //public static CommandParser cmdParser = new CommandParser();

        static async Task Main(string[] args)
        {
            GridBot Gunnar = new GridBot(5, 5);
            // TxtParser TParser = new TxtParser(Gunnar);
            // TParser.RunFile("robot.txt");

            AppCmdParser appCmdParser = new AppCmdParser(Gunnar);
            
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseWebSockets();

            app.Use(async (context, next) =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                        {
                            // WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                            // webSocketHandler.HandleWebSocketAsync(webSocket, appCmdParser);
                            Gunnar.StartSocket(context);

                        } else {
                                await next();
                        }
                });
        
            await app.RunAsync();


            }



}
}