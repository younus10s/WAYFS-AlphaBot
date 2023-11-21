using System.Net.WebSockets;

namespace ConsoleApplication
{
class Program {

    public static string clientMessage = "";
    public static WebSocketHandler webSocketHandler = new WebSocketHandler();

        static async Task Main(string[] args)
        {

        Console.WriteLine("test");
        double Power = 0.4;
        bool Calibrate = true;
        int Rows = 5;
        int Cols = 5;

        GridBot Gunnar = new GridBot(Power, Calibrate, Rows, Cols);
        //TxtParser TParser = new TxtParser();

        //await TParser.RunFile("robot.txt", Gunnar); 

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

            Gunnar.CleanUp();

            }
            
    }
}
