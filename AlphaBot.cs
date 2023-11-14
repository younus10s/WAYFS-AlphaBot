/* A class that represent the robot. Accessing low level 
 * functionality (like movement and sensors) are done through this class.
 * 
 * TurnLeft() & TurnRight()
 * Function that make the robot, turn in a black tape crossing, 90 degrees to the left or right respectively.
 * 
 * LineFollow()
 * A funciton that makes the robot follow a black line until there is a tape crossing. 
 * Return true if the robot is moving elsewere false.
 * 
 * CleanUp()
 * Calls MotionControl to stop both left and right motor, should be called after running. 
 */

public class AlphaBot
{
    public MotionControl MotionControl = new MotionControl();
    public TRSensor TRSensor = new TRSensor();
    public Camera Camera = new Camera();

    private double Power;

    public AlphaBot(double power, bool Calibrate) {
        Power = power;

        if (Calibrate) {
            TRSensor.Calibrate(MotionControl);
        }
    }

    public void TurnLeft() {
        int[] SensorValues = TRSensor.ReadLine();
        MotionControl.Left(Power/2);

        while(SensorValues[1]==1) {
            SensorValues = TRSensor.ReadLine();
        }
        while(SensorValues[1]==0) {
            SensorValues = TRSensor.ReadLine();
        }
        MotionControl.Stop();
    }

    public void TurnRight() {
        int[] SensorValues = TRSensor.ReadLine();
        MotionControl.Right(Power/2);
        
        while(SensorValues[3]==1) {
            SensorValues = TRSensor.ReadLine();
        }
        while(SensorValues[3]==0) {
            SensorValues = TRSensor.ReadLine();
        }
        MotionControl.Stop();
    }

    public void LineFollow()
    {        
        double ScalingFactor = 100;

        int MemorySize = 10;
        double[] PositionMemory = new double[MemorySize];
        double Derivative;
        double Integral = 0;

        double PositionParameter   = 0.005;
        double IntegralParameter   = 0.0001;
        double DerivativeParameter = 0.05/MemorySize; 

        double SteeringInput;

        MotionControl.Forward(Power);

        while (Following()) {

            for (int i = MemorySize-1; i > 0; i--)
            {
                PositionMemory[i] = PositionMemory[i-1];
            }

            PositionMemory[0] = TRSensor.GetPosition();

            Derivative = 0;

            for (int i = 1; i < MemorySize; i++)
            {
                Derivative += (PositionMemory[0] - PositionMemory[i]);
            }

            Integral += PositionMemory[0];

            SteeringInput = PositionMemory[0] * PositionParameter 
                          + Integral * IntegralParameter 
                          + Derivative * DerivativeParameter;

            Steer(SteeringInput/ScalingFactor);
        }

        MotionControl.Stop();
    }

    private void Steer(double SteeringInput)
    {
        if (SteeringInput > Power) {
            SteeringInput = Power;
        }
        if (SteeringInput < -Power) {
            SteeringInput = -Power;
        }
        if (SteeringInput < 0) {
            MotionControl.SetPowerLeft(Power + SteeringInput);
            MotionControl.SetPowerRight(Power);
        } else {
            MotionControl.SetPowerLeft(Power);
            MotionControl.SetPowerRight(Power - SteeringInput);
        }
    }

    private bool Following()
    {
        int[] SensorValues = TRSensor.ReadLine();

        if (SensorValues.Sum() >= 3){
            return false;
        }
        if(SensorValues.Sum() == 0) {
            MotionControl.Stop(); 
            throw new OffLineException("Following: No line!");
        }

        return true;
    }

    public void CleanUp() {
        MotionControl.CleanUp();
    }
}

public class OffLineException : Exception
{
    public OffLineException()
    {
    }

    public OffLineException(string message)
        : base(message)
    {
    }

    public OffLineException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
