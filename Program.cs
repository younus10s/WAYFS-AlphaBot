using System.Net.WebSockets;
using CommandLine;

public class Options
{
    [Option('t', "txt", Required = false, HelpText = "Set to run using txt-file parser.")]
    public string TxtFile { get; set; }

    [Option('u', "urls", Required = false, HelpText = "Provide URL for Web Sockets.")]
    public string URL { get; set; }

    [Option('d', "dummy", Required = false, HelpText = "Run Program in Dummy Mode.")]
    public bool Dummy { get; set; }
}

namespace ConsoleApplication
{
    class Program
    {
        static double Power = 0.4;
        static bool Calibrate = true;
        static int Rows = 5;
        static int Cols = 5;

        static async Task Main(string[] args)
        {

            var parserResult = Parser.Default.ParseArguments<Options>(args);

            await parserResult.WithParsedAsync(async options =>
            {

                if (options.URL != null)
                {
                    Console.WriteLine($"Using URL: {options.URL}");

                    await WebSocketRoutine(options.Dummy, options.URL);
                }
                else if (options.TxtFile != null)
                {
                    Console.WriteLine($"Using txtParser to run file: {options.TxtFile}");

                    await TxtParserRoutine(options.TxtFile);
                }
            });
        }

        private static async Task TxtParserRoutine(string FileName)
        {
            GridBot Gunnar = new GridBot(Power, Calibrate, Rows, Cols);
            TxtParser TParser = new TxtParser();

            await TParser.RunFile(FileName, Gunnar);

            Gunnar.CleanUp();
        }

        private static async Task WebSocketRoutine(bool Dummy, string URL)
        {
            AppCmdParser cmdParser = null;
            GridBot Gunnar;

            if (!Dummy)
            {
                Gunnar = new GridBot(Power, Calibrate, Rows, Cols);
                cmdParser = new AppCmdParser(Gunnar);
            }

            string[] args = { "--urls", URL };

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseWebSockets();

            Console.WriteLine("Test4");

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Test5");
                if (context.WebSockets.IsWebSocketRequest)
                {

                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    Console.WriteLine("Test6");


                    Console.WriteLine("The frontend is connected");
                    WebSocketHandler webSocketHandler = new WebSocketHandler(webSocket);

                    await webSocketHandler.HandleWebSocketAsync(cmdParser);

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
