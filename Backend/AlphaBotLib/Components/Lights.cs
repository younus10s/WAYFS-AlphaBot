/* Class Lights
 * Handles displaying colors for the four LED lights on the bottom of the robot.
 * 
 * ShowAll(Color color)
 * Sets and shows all lights in the strip to the provided color.
 *
 * SwitchOff()
 * Resets the strip and sets all LEDs to Black? RGB 0,0,0.
 *
 * StartColorWipe()
 * Creates and starts a Thread for running ColorWipe().
 *
 * ColorWipe()
 * Sets all LED lights first to red, then green, then blue and waits before changing color.
 * Uses Wipe(Color color) to affect all pixels.
 * Uses SwitchOff() to reset the lights after use.
 * 
 * Wipe(Color color)
 * Sets all LEDs to the chosen Color, one after the other with a delay. 
 *
 * StartBlinking(int[] ledIDs, Color color)
 * Creates a new Thread to use the functionality of Blink(ledIDs, color).
 *
 * StopBlinking()
 * Joins the thread created in StartBlinking and stops the lights.
 *
 * Blink(int[] ledID, Color color)
 * Update loop that tracks the state of the IsBlinking flag.
 * Uses SetLEDSAndWait(ledIDs, color) to specify the lights and color to use.
 *
 * SetLEDSAndWait(ledIDs, color)
 * Set the color to all lights with an ID found in ledIDs and then show the result for the specified Interval.
 */

using rpi_ws281x;
using System.Drawing;

public class Lights : ILights
{
    public const int LedCount = 4;
    public bool IsOn { get; private set; } = false;
    
    private bool IsBlinking; // public ? or keep private?
    private Thread? Thread;
    private int Interval = 500;  // Blink every 500 milliseconds

    private WS281x Device { get; set; }
    private Controller Controller { get; set; }
    private Color[] Empty { get; set; }

    public Lights()
    {
        var settings = Settings.CreateDefaultSettings();
        settings.AddController(LedCount, Pin.Gpio18, StripType.WS2811_STRIP_RGB);

        Device = new(settings);
        Controller = Device.GetController();

        // Clear all LEDs with black color
        Empty = Enumerable.Repeat(Color.FromArgb(0, 0, 0), LedCount).ToArray();
    }

    public void ShowAll(Color color)
    {
        SwapRedAndGreen(ref color);

        Controller.SetLED(0, color);
        Controller.SetLED(1, color);
        Controller.SetLED(2, color);
        Controller.SetLED(3, color);

        Device.Render();
        IsOn = true;
    }

    public void SwitchOff()
    {
        if (IsOn)
        {
            Controller.SetLEDS(Empty);
            Device.Reset(); // no danger to Reset device multiple times?

            IsOn = false;
        }

        Thread?.Join();  // Wait for the thread to finish
    }

    public void StartColorWipe(uint playCount = 1)
    {
        Thread = new (() => ColorWipe(playCount));
        Thread.Start();
    }

    private void ColorWipe(uint playCount)
    {
        IsOn = true;

        for (var i = 0; i < playCount; i++)
        {
            Wipe(Color.Red);
            Wipe(Color.Green);
            Wipe(Color.Blue);
        }

        SwitchOff();
    }

    private void Wipe(Color color)
    {
        SwapRedAndGreen(ref color);

        for (var i = 0; i < LedCount; i++)
        {
            Controller.SetLED(i, color);
            Device.Render();
            var waitPeriod = (int)Math.Max(500.0 / LedCount, 5.0);
            Thread.Sleep(waitPeriod);
        }
    }

    public void StartBlinking(int[] ledIDs, Color color)
    {
        IsBlinking = true;
        Thread = new (() => Blink(ledIDs, color));
        Thread.Start();
    }

    public void StopBlinking()
    {
        IsBlinking = false;
    }

    private void Blink(int[] ledIDs, Color color)
    {
        while (IsBlinking)
        {
            SetLEDSAndWait(ledIDs, color);
            SetLEDSAndWait(ledIDs, Color.Empty);
        }

        SwitchOff();
    }

    private void SetLEDSAndWait(int[] ledID, Color color)
    {
        for (int i = 0; i < ledID.Length; i++)
        {
            Controller.SetLED(ledID[i], color);
        }

        Device.Render();
        Thread.Sleep(Interval);
    }

    // Found potential bug in rpi_ws281x.Controller so this is a workaround.
    private void SwapRedAndGreen(ref Color color)
    {
        if (color == Color.Red)
        {
            color = Color.Green;
        }
        else if (color == Color.Green)
        {
            color = Color.Red;
        }
    }
}