/* A class that represent the robot. Accessing low level 
 * functionality (like movement and sensors) are done through this class.
 * 
 * TurnLeft() & TurnRight()
 * Function that make the robot, turn in a black tape crossing, 90 degrees to the left or right respectively.
 * 
 * LineFollow()
 * A funciton that makes the robot follow a black line until there is a tape crossing. 
 * Return true if the robot is moving elsewere false
 * 
 * CleanUp()
 * Calls MotionControl to stop both left and right motor, should be called after running. 
 */
class AlphaBot
{
    private MotionControl MotionControl = new MotionControl();
    private TRSensor TRSensor = new TRSensor();
    private double Power;

    public AlphaBot(double power) {
        Power = power;
    }

    public void TurnLeft() {
        int[] SensorValues = TRSensor.ReadLine();
        MotionControl.Left(Power);

        while(SensorValues[2]==1) {
            SensorValues = TRSensor.ReadLine();
        }
        while(SensorValues[2]==0) {
            SensorValues = TRSensor.ReadLine();
        }
        MotionControl.Stop();
    }

    public void TurnRight() {
        int[] SensorValues = TRSensor.ReadLine();
        MotionControl.Right(Power);
        
        while(SensorValues[2]==1) {
            SensorValues = TRSensor.ReadLine();
        }
        while(SensorValues[2]==0) {
            SensorValues = TRSensor.ReadLine();
        }
        MotionControl.Stop();
    }

    public bool LineFollow() {
        int[] forward = {0,0,1,0,0};
        int[] left1 =   {0,0,1,1,0};
        int[] left2 =   {0,0,0,1,0};
        int[] right1 =  {0,1,1,0,0};
        int[] right2 =  {0,1,0,0,0};

        int[] SensorValues;
        MotionControl.Forward(Power);

        bool Continue = true;

        while(Continue) {
            SensorValues = TRSensor.ReadLine();

            if(SensorValues.Sum() >= 3) {
                MotionControl.Stop();
                Continue = false;
            } else if(SensorValues.SequenceEqual(forward)) {
                MotionControl.Forward(Power);
            } else if(SensorValues.SequenceEqual(left1) || SensorValues.SequenceEqual(left2) || SensorValues[4]==1) {
                MotionControl.SetPowerRight(Power*0.9);
            } else if(SensorValues.SequenceEqual(right1) || SensorValues.SequenceEqual(right2) || SensorValues[0]==1) {
                MotionControl.SetPowerLeft(Power*0.9);
            } else {
                Console.WriteLine("Unhandeled case");
                Console.WriteLine("SensonValues: " + string.Join(", ", SensorValues));

                Continue = false;
                return false; 
            }
        }
        return true; 
    }

    public void CleanUp() {
        MotionControl.CleanUp();
    }
}
