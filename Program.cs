using System.Net.WebSockets;

namespace ConsoleApplication
{
    class Program
    {

        static async Task Main(string[] args)
        {
            double Power = 0.4;
            bool Calibrate = true;
            int Rows = 5;
            int Cols = 5;
            
            //public static WebSocketHandler webSocketHandler = new WebSocketHandler();
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
                    Console.WriteLine("The frontend is connected");
                    WebSocketHandler webSocketHandler = new WebSocketHandler(webSocket);
                    await webSocketHandler.HandleWebSocketAsync(cmdParser);
                    //await webSocketHandler.HandleWebSocketAsync();
                    Console.WriteLine("after socket messages");
                }
                else
                {
                    await next();
                }
            });

            await app.RunAsync();
        }
    }
}
