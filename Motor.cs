using System.Device.Gpio;

/* Motor Class.
 * Represents each of the motor's wheels.
 * Each wheel is controlled via RPi's GPIO pins.
 * The in(1/2) pins together set the mode of the wheel (forwards, backwards, stop).
 * To achieve control over the speed PWM is used on the ena-pin.
 * 
 * SetPower(double DutyCycle)
 * 
 * PwmLoop() manually applies values high/low on pins as a solution for.
 * This Class is multithreaded, it creates a spicific thread for the pwm functionality
 */

class Motor 
{
    private readonly GpioController gpioController;
    private readonly int in1;
    private readonly int in2;
    private readonly int ena;

    private Thread? pwmThread;
    private readonly int Frequency;
    private double DutyCycle = 0;
    private volatile bool keepRunning = false;
    private readonly object LockDutyCycle = new();

    public Motor(int in1_, int in2_, int ena_, int frequency) {
        in1 = in1_;
        in2 = in2_;
        ena = ena_;
        Frequency = frequency;
        gpioController = new GpioController();

        gpioController.OpenPin(in1, PinMode.Output);
        gpioController.OpenPin(in2, PinMode.Output);
        gpioController.OpenPin(ena, PinMode.Output);

        StartPwm();
        Stop();
    }

    public void SetPower(double dutyCycle) {
        lock(LockDutyCycle){
            DutyCycle = dutyCycle;
        }
    }

    public void Forward() {
        gpioController.Write(in1, PinValue.Low);
        gpioController.Write(in2, PinValue.High);
    }

    public void Backward() {
        gpioController.Write(in1, PinValue.High);
        gpioController.Write(in2, PinValue.Low);
    }

    public void Stop() {
        lock(LockDutyCycle){
            DutyCycle = 0;
        }
    }

    private void StartPwm() {
        keepRunning = true;
        pwmThread = new Thread(PwmLoop);
        pwmThread.Start();
    }
    
    public void StopPwm() {
        keepRunning = false;
        pwmThread?.Join();
    }

    private void PwmLoop() {
        double tempDutyCycle = 0;

        while (keepRunning) {
            lock(LockDutyCycle) {
                tempDutyCycle = DutyCycle;
            }
            int period = (int)(1000.0 / Frequency);
            int pulseWidth = (int)(period * tempDutyCycle);

            gpioController.Write(ena, PinValue.High);
            Thread.Sleep(pulseWidth);

            gpioController.Write(ena, PinValue.Low);
            Thread.Sleep(period - pulseWidth);
        }
    }
}
