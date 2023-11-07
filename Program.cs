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
    public static WebSocketHandler webSocketHandler = new WebSocketHandler();

    static async void Main(string[] args)
    {
        GridBot Gunnar = new GridBot(5, 5);
        TxtParser TParser = new TxtParser(Gunnar);
        TParser.RunFile("robot.txt");

        // Gunnar.Place(2,1,"north");
        // Console.WriteLine("Forwards");
        // Gunnar.Move();
        // Gunnar.Report(); 
        // Console.WriteLine("Left");
        // Gunnar.Left();
        // Console.WriteLine("Forwards");
        // Gunnar.Move();
        // Gunnar.Report(); 
        // Console.WriteLine("Right");
        // Gunnar.Right();
        // Console.WriteLine("Forwards");
        // Gunnar.Move();
        // Gunnar.Report(); 

        Gunnar.CleanUp();

        //Program.startApp(args);
       
        
    }

    public async void startApp(string [] args){
        var builder = WebApplication.CreateBuilder(args);
                    var app = builder.Build();

                    app.UseWebSockets();

                    app.Use(async (context, next) =>
                    {
                        if (context.WebSockets.IsWebSocketRequest)
                        {
                            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                            await webSocketHandler.HandleWebSocketAsync(webSocket);
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