/* Class Buzzer
 * Class to control the output to the Buzzer component on the top of the robot.
 * 
 * BeepOn()
 * Start the buzzing sound.
 *
 * BeepOff()
 * Stop the buzzing sound.
 *
 * Dispose()
 * Dispose method to release resources when the Buzzer is no longer needed.
 *
 * GetIsBeeping()
 * Checks if the PinValue is High to determine if the buzzer is on.
 */

using System.Device.Gpio;

public class Buzzer : IDisposable
{
    public bool IsBepping
    {
        get { return GetIsBepping(); }
    }
    private readonly int PinNumber = 4; // double-check

    private readonly GpioController Controller;

    public Buzzer()
    {
        Controller = new();
        Controller.OpenPin(PinNumber, PinMode.Output);
    }

    public void BeepOn()
    {
        Console.WriteLine("Buzzer On");
        Controller.Write(PinNumber, PinValue.High);
    }

    public void BeepOff()
    {
        Console.WriteLine("Buzzer Off");
        Controller.Write(PinNumber, PinValue.Low);
    }

    public void Dispose()
    {
        Controller.ClosePin(PinNumber); // not sure if neccesary
        Controller.Dispose();
    }

    private bool GetIsBepping()
    {
        return Controller.Read(PinNumber) == PinValue.High;
    }
 }