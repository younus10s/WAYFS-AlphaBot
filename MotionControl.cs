/* Class MotionControl
 * 
 * Motion control methods for AlphaBot implementing from Motor for added accuracy. 
 * Each wheel is represented seperatly with LeftMoter and RightMotor and can be accessed through this class.
 * 
 * Stop()
 * Stops the robot
 * 
 * Forward(double Power)
 * Makes robot go straight at set Power
 * 
 * Backward(double Power)
 * Makes robot go backwards at set Power
 * 
 * Left(double Power) & Right(double Power)
 * Makes the robot turn in place either left or right at set Power
 * 
 * SetPowerLeft(double Power) & SetPowerRight(double Power)
 * Sets power to each wheel individually.
 *
 * CleanUp()
 * Should be ran when MotionControl goes out of scope to stop both motors. 
 */
public class MotionControl
{
    private const int left_in1 = 12;
    private const int left_in2 = 13;
    private const int left_ena = 6;
    private const int right_in1 = 20;
    private const int right_in2 = 21;
    private const int right_ena = 26;

    private const int Frequency = 50;
    private readonly Motor LeftMotor;
    private readonly Motor RightMotor;

    public MotionControl() {
        LeftMotor = new Motor(left_in1, left_in2, left_ena, Frequency);
        RightMotor = new Motor(right_in1, right_in2, right_ena, Frequency);
    }

    public void CleanUp() {
        RightMotor.StopPwm();
        LeftMotor.StopPwm();
    }

    public void Stop() {
        LeftMotor.Stop();
        RightMotor.Stop();

        RightMotor.SetPower(0);
        LeftMotor.SetPower(0);
    }

    public void Forward(double power) {
        LeftMotor.Forward();
        RightMotor.Forward();

        RightMotor.SetPower(power);
        LeftMotor.SetPower(power);
    }

    public void Backward(double power) {
        LeftMotor.Backward();
        RightMotor.Backward();

        RightMotor.SetPower(power);
        LeftMotor.SetPower(power);
    }

    public void Left(double power) {
        LeftMotor.Backward();
        RightMotor.Forward();

        LeftMotor.SetPower(power);
        RightMotor.SetPower(power);
    }

    public void Right(double power) {
        LeftMotor.Forward();
        RightMotor.Backward();

        LeftMotor.SetPower(power);
        RightMotor.SetPower(power);
    } 

    public void SetPowerLeft(double power) {
        LeftMotor.Forward();
        LeftMotor.SetPower(power);
    }

    public void SetPowerRight(double power) {
        RightMotor.Forward();
        RightMotor.SetPower(power);
    }
}
