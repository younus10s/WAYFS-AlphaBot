/* Class Lights
 * Handles displaying colors for the four LED lights on the bottom of the robot.
 * 
 * ShowAll(Color color)
 * Sets and shows all lights in the strip to the provided color.
 *
 * ColorWipe()
 * Sets all LED lights first to red, then green, then blue and waits before changing color.
 * Uses Wipe(Color color) to affect all pixels.
 * Uses SwitchOff() to reset the lights after use.
 * 
 * Wipe(Color color)
 * Sets all LEDs to the chosen Color, one after the other with a delay.
 * 
 * SwitchOff()
 * Resets the strip and sets all LEDs to Black? RGB 0,0,0.
 *
 * StartBlinking(int[] ledIDs, Color color)
 * Creates a new Thread to use the functionality of Blink(ledIDs, color).
 *
 * StopBlinking()
 * Joins the thread created in StartBlinking and stops the lights.
 *
 * Blink(int[] ledID, Color color)
 * Update loop that tracks the state of the _isBlinking flag.
 * Uses SetLEDSAndWait(ledIDs, color) to specify the lights and color to use.
 *
 * SetLEDSAndWait(ledIDs, color)
 * Set the color to all lights with an ID found in ledIDs and then show the result for the specified _interval.
 */

using rpi_ws281x;
using System.Drawing;

public class Lights
{
    private bool _isBlinking;
    private bool _isOn = false;
    private Thread? _blinkThread;
    private int _interval = 500;  // Blink every 500 milliseconds

    private WS281x Device { get; set; }
    private Controller Controller { get; set; }
    private const int _LEDCount = 4; // keep public?
    private Color[] Empty { get; set; }

    public Lights()
    {
        var settings = Settings.CreateDefaultSettings();
        settings.AddController(_LEDCount, Pin.Gpio18, StripType.WS2811_STRIP_RGB);

        Device = new(settings);
        Controller = Device.GetController();

        Empty = new[] // Clear all 4 LEDs , can also be dynamic.
        {
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(0, 0, 0)
        };
    }

    public void ShowAll(Color color)
    {
        Controller.SetLED(0, color);
        Controller.SetLED(1, color);
        Controller.SetLED(2, color);
        Controller.SetLED(3, color);

        Device.Render();
        _isOn = true;
    }

    public void ColorWipe()
    {
        for (var i = 0; i < _LEDCount; i++)
        {
            Wipe(Color.Red);
            Wipe(Color.Green);
            Wipe(Color.Blue);
        }

        SwitchOff();
    }

    private void Wipe(Color color)
    {
        for (var i = 0; i < Controller.LEDCount; i++)
        {
            Controller.SetLED(i, color);
            Device.Render();
            var waitPeriod = (int)Math.Max(500.0 / Controller.LEDCount, 5.0);
            Thread.Sleep(waitPeriod);
        }
    }

    public void SwitchOff()
    {
        if (_isOn)
        {
            Controller.SetLEDS(Empty);
            Device.Reset(); // no danger to Reset device multiple times?

            _isOn = false;
        }
    }
    public void StartBlinking(int[] ledIDs, Color color)
    {
        _isBlinking = true;
        _blinkThread = new Thread(() => Blink(ledIDs, color));
        _blinkThread.Start();
    }

    public void StopBlinking()
    {
        _isBlinking = false;
        _blinkThread?.Join();  // Wait for the thread to finish
    }

    private void Blink(int[] ledIDs, Color color)
    {
        while (_isBlinking)
        {
            SetLEDSAndWait(ledIDs, color);
            SetLEDSAndWait(ledIDs, Color.Empty); // same as Empty[0] ?
        }

        SwitchOff(); // maybe not thread safe? move to StopBlinking?
    }

    private void SetLEDSAndWait(int[] ledID, Color color)
    {
        for (int i = 0; i < ledID.Length; i++)
        {
            Controller.SetLED(ledID[i], color);
        }

        Device.Render();
        Thread.Sleep(_interval);
    }
}