using System.Drawing;

public class LightBot : IRobotCommands
{
    public int PosX { get; set; } = 0;
    public int PosY { get; set; } = 0;
    public string Heading { get; set; } = "";
    
    private Lights Lights { get; set; }
    private Color Color { get; set; }
    
    public LightBot()
    {
        // Do my setup here.
        Lights = new();
    }

    public void Place(int x, int y, string heading)
    {
        Console.WriteLine($"Handling `Place` command. Lights are on? {Lights.IsOn}");
    }

    public void Move()
    {
        // Test 1: Everything is fine.
        Color = Color.Green;
        
        Console.WriteLine($"Handling `Move` command. Lights are on? {Lights.IsOn}");
        Console.WriteLine($"Showing {Lights.LedCount} lights with color {Color}.");

        Lights.ShowAll(Color);
        
        // Test 2: We get an exception.
        try
        {
            Lights.ShowAll(Color);
            throw new OffLineException($"Throwing `OffLineException` to test the error lights.");
            
        }
        catch (OffLineException e)
        {
            Console.WriteLine(e);
            Lights.ShowAll(Color.Red);
        }
    }

    public void Left()
    {
        Color = Color.Orange;
        var ledIDs = new int[] { 2, 3 };
        
        Console.WriteLine($"Handling `Left` command. Lights are on? {Lights.IsOn}");
        Console.WriteLine($"Blinking {ledIDs.Length} lights with color {Color}.");

        Lights.StartBlinking(ledIDs, Color);
        Lights.StopBlinking();
    }

    public void Right()
    {
        Color = Color.DeepPink;
        var ledIDs = new int[] { 0, 1 };
        
        Console.WriteLine($"Handling `Right` command. Lights are on? {Lights.IsOn}");
        Console.WriteLine($"Blinking {ledIDs.Length} lights with color {Color}.");

        Lights.StartBlinking(ledIDs, Color);
        Lights.StopBlinking();
    }

    public Task Report()
    {        
        Console.WriteLine($"Handling `Report` command. Lights are on? {Lights.IsOn}");
        Console.WriteLine($"Showing the `ColorWipe` animation. Enjoy the light show!");

        Lights.StartColorWipe();

        return Task.CompletedTask;
    }
}