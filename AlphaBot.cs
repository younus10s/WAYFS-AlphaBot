using System.Device.Gpio;

class AlphaBot
{
    private const int left_in1 = 12;
    private const int left_in2 = 13;
    private const int left_ena = 6;
    private const int right_in1 = 20;
    private const int right_in2 = 21;
    private const int right_ena = 26;
  
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

        gpioController.Write(left_ena, PinValue.High);
        gpioController.Write(right_ena, PinValue.High);
        Stop();
    }

    public void Forward()
    {
        gpioController.Write(left_in1, PinValue.High);
        gpioController.Write(left_in2, PinValue.Low);

        gpioController.Write(right_in1, PinValue.High);
        gpioController.Write(right_in2, PinValue.Low);
    }

    public void Stop()
    {
        gpioController.Write(left_in1, PinValue.Low);
        gpioController.Write(left_in2, PinValue.Low);
        gpioController.Write(right_in1, PinValue.Low);
        gpioController.Write(right_in2, PinValue.Low);
    }
}