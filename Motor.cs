using System.Device.Gpio;

class Motor 
{
    private GpioController gpioController;
    private int in1;
    private int in2;
    private int ena;

    private Thread? pwmThread;
    private int frequency;
    private  double dutyCycle = 0;
    private volatile bool keepRunning = false;
    private object lockDutyCycle = new object();

    public Motor(int in1_, int in2_, int ena_, int frequency_)
    {
        in1 = in1_;
        in2 = in2_;
        ena = ena_;
        frequency = frequency_;

        gpioController = new GpioController();

        gpioController.OpenPin(in1, PinMode.Output);
        gpioController.OpenPin(in2, PinMode.Output);
        gpioController.OpenPin(ena, PinMode.Output);

        StartPwm();
        Stop();
    }

    public void SetPower(double power){
        lock(lockDutyCycle){
            dutyCycle = power;
        }
    }

    public void Forward()
    {
        gpioController.Write(in1, PinValue.Low);
        gpioController.Write(in2, PinValue.High);
    }

    public void Backward()
    {
        gpioController.Write(in1, PinValue.High);
        gpioController.Write(in2, PinValue.Low);
    }

    public void Stop()
    {
        gpioController.Write(in1, PinValue.Low);
        gpioController.Write(in2, PinValue.Low);
    }


    public void StartPwm()
    {
            keepRunning = true;
            pwmThread = new Thread(PwmLoop);
            pwmThread.Start();
    }
    
    public void StopPwm()
    {
        keepRunning = false;
        pwmThread?.Join();
	Console.WriteLine("Got Here");
    }

    public void PwmLoop()
    {
        double tempDutyCycle = 0;

        while (keepRunning)
        {

            lock(lockDutyCycle)
            {
                tempDutyCycle = dutyCycle;
            }
            int period = (int)(1000.0 / frequency);
            int pulseWidth = (int)(period * tempDutyCycle);

            gpioController.Write(ena, PinValue.High);
            Thread.Sleep(pulseWidth);

            gpioController.Write(ena, PinValue.Low);
            Thread.Sleep(period - pulseWidth);
        }
    }
}
