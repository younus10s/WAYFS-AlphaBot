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

        while(SensorValues[2]==1) {
            SensorValues = TRSensor.ReadLine();
        }
        while(SensorValues[0]==0) {
            SensorValues = TRSensor.ReadLine();
        }
        MotionControl.Stop();
    }

    public void TurnRight() {
        int[] SensorValues = TRSensor.ReadLine();
        MotionControl.Right(Power/2);
        
        while(SensorValues[2]==1) {
            SensorValues = TRSensor.ReadLine();
        }
        while(SensorValues[4]==0) {
            SensorValues = TRSensor.ReadLine();
        }
        MotionControl.Stop();
    }

    public void LineFollow()
    {
        double ScalingFactor = 200;

        double X = 50*ScalingFactor;
        double Y = 10000*ScalingFactor;
        double Z = 10*ScalingFactor;

        double Position;
        double Derivative;
        double Integral = 0;

        double LastPosition = 0;

        double SteeringInput;

        while (Following()) {
            Position = TRSensor.GetPosition();

            Derivative = Position - LastPosition;
            Integral += Position;

            SteeringInput = Position / X + Integral / Y + Derivative / Z;

            Console.WriteLine("Position: " + Position + ", Steering: " + SteeringInput);
            Console.WriteLine("POS: " + Position / X + ", INT: " + Integral / Y + ", DER: " + Derivative / Z);

            Steer(SteeringInput);

            LastPosition = Position;
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
            CleanUp();
            throw new Exception("Can not find the line any more!");
            return false;
        }

        return true;
    }

    public void CleanUp() {
        MotionControl.CleanUp();
    }
}
