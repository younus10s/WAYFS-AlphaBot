using System.Device.Gpio;
using System.Threading;

class Movement
{
    private const int left_in1 = 12;
    private const int left_in2 = 13;
    private const int left_ena = 6;
    private const int right_in1 = 20;
    private const int right_in2 = 21;
    private const int right_ena = 26;
  
    private const int frequency = 400;
    private  double dutyCycle = 0.5;
    private volatile bool keepRunning = false;
    private GpioController gpioController;

    private Thread? pwmThread;
    private object lockDutyCycle = new object();


    public Movement()
    {
        gpioController = new GpioController();

        gpioController.OpenPin(left_in1, PinMode.Output);
        gpioController.OpenPin(left_in2, PinMode.Output);
        gpioController.OpenPin(left_ena, PinMode.Output);
        gpioController.OpenPin(right_in1, PinMode.Output);
        gpioController.OpenPin(right_in2, PinMode.Output);
        gpioController.OpenPin(right_ena, PinMode.Output);

        StartPwm();
        Stop();
    }

    ~Movement(){
        StopPwm();
    }

    public void Forward()
    {
        lock(lockDutyCycle){
            dutyCycle = 0.5;
        }

        gpioController.Write(left_in1, PinValue.High);
        gpioController.Write(left_in2, PinValue.Low);
        gpioController.Write(right_in1, PinValue.High);
        gpioController.Write(right_in2, PinValue.Low);
    }

    public void Stop()
    {
        lock(lockDutyCycle){
            dutyCycle = 0;
        }
        
        gpioController.Write(left_in1, PinValue.Low);
        gpioController.Write(left_in2, PinValue.Low);
        gpioController.Write(right_in1, PinValue.Low);
        gpioController.Write(right_in2, PinValue.Low);
    }

    public void Backward()
    {
        gpioController.Write(left_in1, PinValue.Low);
        gpioController.Write(left_in2, PinValue.High);
        gpioController.Write(right_in1, PinValue.Low);
        gpioController.Write(right_in2, PinValue.High);
    }

    public void Left()
    {
        gpioController.Write(left_in1, PinValue.Low);
        gpioController.Write(left_in2, PinValue.High);
        gpioController.Write(right_in1, PinValue.High);
        gpioController.Write(right_in2, PinValue.Low);
    }

    public void Right()
    {
        gpioController.Write(left_in1, PinValue.High);
        gpioController.Write(left_in2, PinValue.Low);
        gpioController.Write(right_in1, PinValue.Low);
        gpioController.Write(right_in2, PinValue.High);
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
    }

    public void PwmLoop()
    {
        double tempDutyCycle = 0;   

        //KeepRunnig maybe needs a lock???
        while (keepRunning)
        {
            if(dutyCycle != 0){
                
                lock(lockDutyCycle){
                    tempDutyCycle = dutyCycle;
                }
                int period = (int)(1000.0 / frequency);
                int pulseWidth = (int)(period * tempDutyCycle);

                gpioController.Write(left_ena, PinValue.High);
                gpioController.Write(right_ena, PinValue.High);
                Thread.Sleep(pulseWidth);

                if(tempDutyCycle != 1){
                    gpioController.Write(left_ena, PinValue.Low);
                    gpioController.Write(right_ena, PinValue.Low);
                    Thread.Sleep(period - pulseWidth);
                }
                
            }else{
                gpioController.Write(left_ena, PinValue.Low);
                gpioController.Write(right_ena, PinValue.Low);
                Thread.Sleep(100);
            }
        }
    }
}