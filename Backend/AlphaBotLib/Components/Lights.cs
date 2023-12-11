/* Class Lights
 * Handles displaying colors for the four LED lights on the bottom of the robot.
 * 
 * Show(Color color)
 * Sets and shows all lights in the strip to the provided color.
 *
 * ClearStrip()
 * Turn off all the lights. 
 */

using rpi_ws281x;
using System.Drawing;

public class Lights
{
    private WS281x Device { get; set; }
    private Controller Controller { get; set; }
    private const int LedCount = 4;

    public Lights()
    {
        var settings = Settings.CreateDefaultSettings();
        settings.AddController(LedCount, Pin.Gpio18, StripType.WS2811_STRIP_RGB);

        Device = new(settings);
        Controller = Device.GetController();
    }

    public void Show(Color color)
    {
        SwapRedAndGreen(ref color);

        SetAll(color);
        Device.Render();
    }

    private void SetAll(Color color)
    {
        Controller.SetLED(0, color);
        Controller.SetLED(1, color);
        Controller.SetLED(2, color);
        Controller.SetLED(3, color);
    }

    public void ClearStrip()
    {
        Device.Reset();
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