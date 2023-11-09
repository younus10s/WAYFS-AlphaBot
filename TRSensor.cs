using System.Device.Gpio;

/* Class TRSensor
 * Class to handle sensor input from the IR Sensors on the bottom of the robot.
 * 
 * AnalogRead() 
 * Reads values from infrared sensors. Returns raw data as an int vector of size 5
 * 
 * ReadLine()
 * Applies thresholding to the data recieved from the sensors to
 * destinguish a black line from another material under he robot.
 * Returns binary data, where one means a black line is detected
 */ 
public class TRSensor {
	private const int CS = 5;
	private const int Clock = 25;
	private const int Address = 24;
	private const int DataOut = 23;
	private int NumSensors = 5;
	private GpioController GpioController;

	private bool Calibrated = false;

	private int MinReading;
	private int MaxReading;

	public TRSensor() {
		GpioController = new GpioController();
		GpioController.OpenPin(Clock, PinMode.Output);
		GpioController.OpenPin(Address, PinMode.Output);
		GpioController.OpenPin(CS, PinMode.Output);
		GpioController.OpenPin(DataOut, PinMode.InputPullUp);
	}

	//hitta max & min
	public void Calibrate(MotionControl MotionControl)
	{
		MotionControl.Left(0.3);

		for (int i = 0; i < 100; i++) {
			int[] Values = AnalogRead();

            Console.WriteLine("Values: " + string.Join(", ", Values));

            TmpMax = Values.Max();
			TmpMin = Values.Min();

			MaxReading = (TmpMax > MaxReading) ? TmpMax : MaxReading;
            MinReading = (TmpMin > MinReading) ? TmpMin : MinReading;
        }

		Console.WriteLine("Done! Put me back please :D");
		string derp = Console.ReadLine();

		Console.WriteLine("Max: " + MaxReading);
        Console.WriteLine("Min: " + MinReading);

        Calibrated = true;
    }

	public int[] ReadCalbrated()
	{
		if (!Calibrated) {
			throw new Exception("The TRSensor is not calibrated. Exiting...");
		}

		int[] Values = AnalogRead();

		foreach (int i in Values) {
			i = 1000 - ((i - MinReading) * 1000 / MaxReading);
		}

        Console.WriteLine("Calibrated Values: " + string.Join(", ", Values));

        return Values;
	}

	public int[] AnalogRead() {
		int[] Value = new int[NumSensors + 1];

		for (int j = 0; j < NumSensors+1; j++){
			GpioController.Write(CS, PinValue.Low);
			
			for (int i = 0; i < 4; i++){
				if ((((j) >> (3 - i)) & 0x01) != 0){
					GpioController.Write(Address, PinValue.High);
				}
				else{
					GpioController.Write(Address, PinValue.Low);
				}
				
				Value[j] <<= 1;
				
				if(GpioController.Read(DataOut) == PinValue.High){
					Value[j] |= 0x01;
				}
				GpioController.Write(Clock, PinValue.High);
				GpioController.Write(Clock, PinValue.Low);
			}
			for(int i = 0; i < NumSensors+1; i++){
				Value[j] <<= 1;
				
				if(GpioController.Read(DataOut) == PinValue.High){
					Value[j] |= 0x01;
				}
				GpioController.Write(Clock, PinValue.High);
                GpioController.Write(Clock, PinValue.Low);
			}
			Thread.Sleep(1);
			GpioController.Write(CS, PinValue.High);
		}
		return Value[1..];
	}

	public double GetPosition() {
		double Average = 0.0;
		double Sum = 0.0;

		int[] SensorValues = ReadCalbrated();

		int Sensor = 0;
		foreach (int Value in SensorValues) {
			Average += (float) (Value * Sensor * 1000);
			Sum += (float) Value;
			Sensor++;
		}
		double Val = (Average / Sum);

		Console.WriteLine("POS:" + Val);

		return Val;
	}

	public int[] ReadLine() {
		int[] SensorData = AnalogRead();

		int[] ThresholdedData = new int[SensorData.Length];

		for(int i = 0; i < SensorData.Length; i++){
			if(SensorData[i] < 550 && SensorData[i] > 0)
				ThresholdedData[i] = 1;
			else
				ThresholdedData[i] = 0;
		}

		//sensor value test
		//Console.WriteLine("data: \t" + string.Join(", ", SensorData) + 
		//						"\t\t" + string.Join(", ", ThresholdedData));

		return ThresholdedData;
	}
}