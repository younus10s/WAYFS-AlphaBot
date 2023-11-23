using System.Device.Gpio;

/* Class TRSensor
 * Class to handle sensor input from the IR Sensors on the bottom of the robot.
 * 
 * Calibrate()
 * Moves the robot in a circle to find the highest and lowest sensor readings.
 * Uses these valuse as parameters for calibrating sensor reading in ReadCalibrated().
 * Running Calibrate is a prereqiusite for running ReadCalibrated().
 * Robot needs to be on a line.
 * 
 * ReadCalibrated() 
 * Returns Sensor readings on the form int [0,1000]. 0 corresponds to no line, and vice versa.
 * 
 * ReadLine()
 * Applies thresholding to the data recieved from the sensors to
 * destinguish a black line from another material under he robot.
 * Returns binary data, where one means a black line is detected
 */ 
public class TRSensor
{
	private const int CS = 5;
	private const int Clock = 25;
	private const int Address = 24;
	private const int DataOut = 23;
	private readonly int NumSensors = 5;
	private readonly GpioController GpioController;

	private bool Calibrated = false;
	private int MinReading = int.MaxValue;
	private int MaxReading = int.MinValue;

	public TRSensor()
	{
		GpioController = new GpioController();
		GpioController.OpenPin(Clock, PinMode.Output);
		GpioController.OpenPin(Address, PinMode.Output);
		GpioController.OpenPin(CS, PinMode.Output);
		GpioController.OpenPin(DataOut, PinMode.InputPullUp);
	}

	public void Calibrate(MotionControl MotionControl)
	{
		MotionControl.Left(0.3);

		for (int i = 0; i < 100; i++)
		{
			int[] Values = AnalogRead();

            int TmpMax = Values.Max();
			int TmpMin = Values.Min();

			MaxReading = (TmpMax > MaxReading) ? TmpMax : MaxReading;
            MinReading = (TmpMin < MinReading) ? TmpMin : MinReading;
        }

		MotionControl.Stop();

		Console.WriteLine("Done! Put me back please :D");
		Console.WriteLine("Press any Key to continue...");
		Console.ReadLine();

        Calibrated = true;
    }

	public int[] ReadCalbrated()
	{
		if (!Calibrated)
		{
			throw new Exception("The TRSensor is not calibrated. Exiting...");
		}

		int[] Values = AnalogRead();
		int[] CalibratedValues = {0,0,0,0,0};

		for (int i = 0; i < Values.Length; i++)
		{
            Values[i] = (Values[i] > MaxReading) ? MaxReading : Values[i];
            Values[i] = (Values[i] < MinReading) ? MinReading : Values[i];

			CalibratedValues[i] = 1000 - ((Values[i] - MinReading) * 1000 / MaxReading);
		}

        return CalibratedValues;
	}

	public double GetPosition()
	{
		double Average = 0.0;
		double Sum = 0.0;
		int[] SensorValues = ReadCalbrated();

		int Threshold = 200;

		for(int i = 0; i < SensorValues.Length; i++)
		{
			int Value = SensorValues[i];
			Value = (Value > Threshold) ? Value : 0;

			Average += (float) (Value * i * 1000);
			Sum += (float) Value;
		}

		if (Sum == 0)
		{
			throw new Exception("All sensor values are 0. Exiting...");
		}

		return (Average / Sum) - 2000;
	}

	public int[] ReadLine()
	{
		int[] SensorData = AnalogRead();
		int[] ThresholdedData = new int[SensorData.Length];
		int Treshold = 500;

		for(int i = 0; i < SensorData.Length; i++)
		{
			ThresholdedData[i] = (SensorData[i] < Treshold) ? 1 : 0;
		}

		return ThresholdedData;
	}

    private int ReadBitAndShift(int sensorIndex, int currentValue)
    {
        currentValue <<= 1;

        if (GpioController.Read(DataOut) == PinValue.High)
        {
            currentValue |= 0x01;
        }

        GpioController.Write(Clock, PinValue.High);
        GpioController.Write(Clock, PinValue.Low);

        return currentValue;
    }

    private int[] AnalogRead()
    {
        int[] values = new int[NumSensors];

        for (int sensorIndex = 0; sensorIndex < NumSensors; sensorIndex++)
        {
            GpioController.Write(CS, PinValue.Low);

            // Send sensor address
            for (int bitIndex = 3; bitIndex >= 0; bitIndex--)
            {
                GpioController.Write(Address, ((sensorIndex >> bitIndex) & 0x01) == 1 ? PinValue.High : PinValue.Low);

                values[sensorIndex] = ReadBitAndShift(sensorIndex, values[sensorIndex]);
            }

            // Receive sensor value
            for (int i = 0; i < NumSensors; i++)
            {
                values[sensorIndex] = ReadBitAndShift(sensorIndex, values[sensorIndex]);
            }

            // Allow some time before the next sensor
            Thread.Sleep(1);

            GpioController.Write(CS, PinValue.High);
        }

        return values;
    }

    /*    private int[] AnalogRead()
        {
            int[] Value = new int[NumSensors + 1];

            for (int j = 0; j < NumSensors + 1; j++)
            {
                GpioController.Write(CS, PinValue.Low);

                for (int i = 0; i < 4; i++)
                {
                    if ((((j) >> (3 - i)) & 0x01) != 0)
                    {
                        GpioController.Write(Address, PinValue.High);
                    }
                    else
                    {
                        GpioController.Write(Address, PinValue.Low);
                    }

                    Value[j] <<= 1;

                    if(GpioController.Read(DataOut) == PinValue.High)
                    {
                        Value[j] |= 0x01;
                    }

                    GpioController.Write(Clock, PinValue.High);
                    GpioController.Write(Clock, PinValue.Low);
                }
                for(int i = 0; i < NumSensors + 1; i++)
                {
                    Value[j] <<= 1;

                    if(GpioController.Read(DataOut) == PinValue.High)
                    {
                        Value[j] |= 0x01;
                    }

                    GpioController.Write(Clock, PinValue.High);
                    GpioController.Write(Clock, PinValue.Low);
                }

                Thread.Sleep(1);
                GpioController.Write(CS, PinValue.High);
            }

            return Value[1..];
        }*/
}