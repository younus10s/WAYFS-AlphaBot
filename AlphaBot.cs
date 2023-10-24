using System.Device.Gpio;
using System.Device.Pwm; 

class AlphaBot
{
    private const int left_in1 = 12;
    private const int left_in2 = 13;
    private const int left_ena = 6;
    private const int right_in1 = 20;
    private const int right_in2 = 21;
    private const int right_ena = 26;
    private PwmChannel pwm_a; 
    private PwmChannel pwm_b;

  
    private GpioController gpioController;

    public AlphaBot()
    {
        gpioController = new GpioController();

        gpioController.OpenPin(left_in1, PinMode.Output);
        gpioController.OpenPin(left_in2, PinMode.Output);
        gpioController.OpenPin(left_ena, PinMode.Output);
        gpioController.OpenPin(right_in1, PinMode.Output);
        gpioController.OpenPin(right_in2, PinMode.Output);
        gpioController.OpenPin(right_ena, PinMode.Output);

        //gpioController.Write(left_ena, PinValue.High);
        //gpioController.Write(right_ena, PinValue.High);

        pwm_a = PwmChannel.Create(0, 0, 400, 0.01);
        pwm_b = PwmChannel.Create(0, 1, 400, 0.01);
        pwm_a.Start();
        pwm_b.Start();

        //Console.WriteLine("isPinOpen: " + gpioController.IsPinOpen(right_ena));
        //Console.WriteLine(gpioController.Read(right_ena));

        Stop();
    }

    public void Forward()
    {
        // pwm_a.DutyCycle = pa;
        // pwm_b.DutyCycle = pb;

        gpioController.Write(left_in1, PinValue.High);
        gpioController.Write(left_in2, PinValue.Low);
        gpioController.Write(right_in1, PinValue.High);
        gpioController.Write(right_in2, PinValue.Low);

        Console.WriteLine("pwm_a.DutyCycle: " + pwm_a.DutyCycle);
    }

    public void Stop()
    {
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
}