using System.Drawing;

public class LightBot : IRobotCommands
{
    public int PosX { get; set; } = 0;
    public int PosY { get; set; } = 0;
    public string Heading { get; set; } = "";
    
    private Lights Lights { get; set; }
    private Color Color { get; set; }

    private int Sleep = 1500;
    private bool Running = true;
    
    public LightBot()
    {
        // Do my setup here.
        Lights = new();
    }

    public void Place(int x, int y, string heading)
    {
        // Console.WriteLine($"Handling 'Place' command. Lights are {(Lights.IsOn ? "on" : "off")}.");

        // Main loop to cycle the different commands.
        Running = true;

        while (Running)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Move");
            Console.WriteLine("2. Left");
            Console.WriteLine("3. Right");
            Console.WriteLine("4. Report");
            Console.WriteLine("5. Quit");

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            Console.Clear(); // Clear the console for a cleaner display

            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                    Move();
                    break;

                case ConsoleKey.A:
                    Left(); // Blink orange on left side
                    break;

                case ConsoleKey.D:
                    Right(); // Blink in different color
                    break;

                case ConsoleKey.S:
                    Report(); // ColorWipe is working
                    break;

                case ConsoleKey.Q:
                    Console.WriteLine("Quitting...");
                    Running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }

            Console.Clear(); // Clear the console before the next iteration
        }
    }

    public void Move()
    {
        Console.WriteLine($"Handling 'Move' command. Lights are {(Lights.IsOn ? "on" : "off")}.");

        ShowAll();
        WaitForUserInput();
        Lights.SwitchOff();

        ShowException();
        WaitForUserInput();
    }

    private void ShowAll()
    {
        Color = Color.Green;        
        
        Console.WriteLine($"Showing {Lights.LedCount} lights with {Color}.");

        Lights.ShowAll(Color);
    }

    private void ShowException()
    {
        Color = Color.Yellow;

        try
        {
            Console.WriteLine($"Showing {Lights.LedCount} lights with color {Color}.");

            Lights.ShowAll(Color);

            WaitForUserInput();
            Lights.SwitchOff();

            throw new OffLineException($"Throwing `OffLineException` to test the error lights.");
        }
        catch (OffLineException e)
        {
            Color = Color.Red;

            Console.WriteLine(e);
            Console.WriteLine($"Showing {Lights.LedCount} lights with color {Color}.");

            Lights.ShowAll(Color);

            WaitForUserInput();
            Lights.SwitchOff();
        }
    }

    public void Left()
    {
        Color = Color.OrangeRed;
        var ledIDs = new int[] { 2, 3 };
        
        Console.WriteLine($"Handling 'Left' command. Lights are {(Lights.IsOn ? "on" : "off")}.");
        Console.WriteLine($"Blinking {ledIDs.Length} lights with color {Color}.");

        Lights.StartBlinking(ledIDs, Color);

        WaitForUserInput();

        Lights.StopBlinking();
    }

    public void Right()
    {
        Color = Color.Purple;
        var ledIDs = new int[] { 0, 1 };
        
        Console.WriteLine($"Handling 'Right' command. Lights are {(Lights.IsOn ? "on" : "off")}.");
        Console.WriteLine($"Blinking {ledIDs.Length} lights with color {Color}.");

        Lights.StartBlinking(ledIDs, Color);

        WaitForUserInput();

        Lights.StopBlinking();
    }

    public Task Report()
    {        
        Console.WriteLine($"Handling 'Report' command. Lights are {(Lights.IsOn ? "on" : "off")}.");
        
        ColorWipe(1);

        return Task.CompletedTask;
    }

    private void ColorWipe(uint playCount)
    {
        Console.WriteLine($"Showing the `ColorWipe` animation. Enjoy the light show!");

        Lights.StartColorWipe(playCount);
    }

    private void WaitForUserInput()
    {
        Thread.Sleep(Sleep);

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}