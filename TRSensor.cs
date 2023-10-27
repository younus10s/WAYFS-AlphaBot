using System.Device.Gpio;

public class TRSensor{
	private const int CS = 5;
	private const int Clock = 25;
	private const int Address = 24;
	private const int DataOut = 23;

	private int numSensors = 5;
	private GpioController gpioController;

	public TRSensor(){
		gpioController = new GpioController();
		gpioController.OpenPin(Clock, PinMode.Output);
		gpioController.OpenPin(Address, PinMode.Output);
		gpioController.OpenPin(CS, PinMode.Output);
		gpioController.OpenPin(DataOut, PinMode.InputPullUp);
	}

	public int[] AnalogRead(){
		int[] Value = new int[numSensors + 1];
		for (int j = 0; j < numSensors+1; j++){
			gpioController.Write(CS, PinValue.Low);
			for (int i = 0; i < 4; i++){
				if((((j) >> (3 - i)) & 0x01) != 0)
					gpioController.Write(Address, PinValue.High);
				else
					gpioController.Write(Address, PinValue.Low);
				Value[j] <<= 1;
				if(gpioController.Read(DataOut) == PinValue.High)
					Value[j] |= 0x01;
				gpioController.Write(Clock, PinValue.High);
				gpioController.Write(Clock, PinValue.Low);
			}
			for(int i = 0; i < 6; i++){
				Value[j] <<= 1;
				if(gpioController.Read(DataOut) == PinValue.High)
					Value[j] |= 0x01;
				gpioController.Write(Clock, PinValue.High);
                                gpioController.Write(Clock, PinValue.Low);
			}
			Thread.Sleep(1);
			gpioController.Write(CS, PinValue.High);
		}
		return Value[1..];
	}

	public void PrintValues(int[] Value){
		Console.Write("[");
		for(int i = 0; i < Value.Length - 1; i++){
			Console.Write(Value[i] + ", ");
		}
		Console.WriteLine(Value[numSensors-1] + "]");
	}

	public int[] ReadLine(int[] Value){
		int[] blackLine = new int[Value.Length];
		for(int i = 0; i < Value.Length; i++){
			if(Value[i] < 400 && Value[i] > 100)
				blackLine[i] = 1;
			else
				blackLine[i] = 0;
		}
		return blackLine;
	}

}
