using System.Net.WebSockets;
using CommandLine;
using System.Diagnostics;
using System;

public class Options
{
    [Option('t', "txt", Required = false, HelpText = "Set to run using txt-file parser.")]
    public string TxtFile { get; set; }

    [Option('u', "urls", Required = false, HelpText = "Provide URL for Web Sockets.")]
    public string URL { get; set; }

    [Option('d', "dummy", Required = false, HelpText = "Run Program in Dummy Mode.")]
    public bool Dummy { get; set; }

    [Option('f', "free", Required = false, HelpText = "Run Program in free movement Mode.")]
    public bool Free { get; set; }
    
    [Option('s', "stream", Required = false, HelpText = "Provide video stream for frontend")]
    public bool Stream { get; set; }
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

                    if (options.Stream)
                    {
                        StartStream();
                    }
                    
                    await WebSocketRoutine(options.Dummy, options.URL, options.Free);
                }
                else if (options.TxtFile != null)
                {
                    Console.WriteLine($"Using txtParser to run file: {options.TxtFile}");

                    await TxtParserRoutine(options.TxtFile);
                }


            });
        }

        private static async Task StartStream()
        {
            string pythonScriptPath = "./stream.py";

            // Set up process start info
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python3",  // or "python3" for Python 3.x
                Arguments = pythonScriptPath,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Start the process
            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                // Read the output (if needed) asynchronously
                Task<string> outputTask = process.StandardOutput.ReadToEndAsync();

                // Continue with other tasks in C# while Python script is running
                Console.WriteLine("C# program continues while Python script is running...");

                // You can await other asynchronous operations here if needed

                // Wait for the Python script to complete and get the output
                string output = await outputTask;
                Console.WriteLine("Python script output:\n" + output);

                process.WaitForExit();
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
            AppCmdParser cmdParser = null;
            GridBot Gunnar = null;
            FreeBot FBot = null;

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
                    if(Free)
                        await webSocketHandler.HandleWebSocketAsyncFreeBot(FBot);
                    else
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
