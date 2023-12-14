using CommandLine;
using System.Diagnostics;
using System.Net.WebSockets;

public class Options
{
    [Option('t', "txt", Required = false, HelpText = "Set to run using txt-file parser.")]
    public string? TxtFile { get; set; }

    [Option('u', "urls", Required = false, HelpText = "Provide URL for Web Sockets.")]
    public string? URL { get; set; }

    [Option('d', "dummy", Required = false, HelpText = "Run Program in Dummy Mode.")]
    public bool Dummy { get; set; }

    [Option('f', "free", Required = false, HelpText = "Run Program in free movement Mode.")]
    public bool Free { get; set; }
}

namespace ConsoleApplication
{
    class Program
    {
        private static Process? PythonStream;

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

                    StartStream();

                    await WebSocketRoutine(options.Dummy, options.URL, options.Free);

                    StopStream();

                }
                else if (options.TxtFile != null)
                {
                    Console.WriteLine($"Using txtParser to run file: {options.TxtFile}");

                    await TxtParserRoutine(options.TxtFile);
                }
            });

        }

        private static void StartStream()
        {
            string pythonScriptPath = "./stream.py";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python3",
                Arguments = pythonScriptPath,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            PythonStream = new Process { StartInfo = startInfo };

            PythonStream.Start();

            Console.WriteLine("C# program continues while Python script is running...");
        }

        private static void StopStream()
        {
            if (PythonStream != null && !PythonStream.HasExited)
            {
                PythonStream.Kill(); // Terminate the process
                PythonStream.Dispose(); // Release resources
            }
        }

        private static async Task TxtParserRoutine(string FileName)
        {
            GridBot Gunnar = new GridBot(Power, Calibrate, Rows, Cols);
            TxtParser TParser = new TxtParser();

            await TParser.RunFile(FileName, Gunnar);
            Gunnar.CleanUp();
        }

        private static async Task WebSocketRoutine(bool Dummy, string URL, bool Free)
        {
            AppCmdParser? cmdParser = null;
            GridBot? Gunnar = null;
            FreeBot? FBot = null;

            if (!Dummy && !Free)
            {
                Gunnar = new GridBot(Power, Calibrate, Rows, Cols);
                cmdParser = new AppCmdParser(Gunnar);
            }
            else if (!Dummy && Free)
            {
                FBot = new FreeBot(Power, Calibrate);
            }

            string[] args = { "--urls", URL };

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
                    if (Free)
                        await webSocketHandler.HandleWebSocketAsyncFreeBot(FBot);
                    else
                        await webSocketHandler.HandleWebSocketAsync(cmdParser);

                    Console.WriteLine("after socket messages");

                    if(Gunnar != null)
                        Gunnar.CleanUp();
                    if(FBot != null)
                        FBot.CleanUp();

                    System.Environment.Exit(0);
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