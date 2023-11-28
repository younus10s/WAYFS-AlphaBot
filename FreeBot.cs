using Microsoft.Net.Http.Headers;
using MMALSharp.Processors.Motion;

public class FreeBot : AlphaBot
{
    public FreeBot(double power, bool Calibrate) : base(power, Calibrate)
    {
        //Maybe add more variable ints
    }

    public void Move(double dx, double dy){
        double abs = Math.Sqrt(dx * dx + dy * dy);
        if(dy > 0.1){
            MotionControl.Forward(abs);
            if(dx > 0.1){
                MotionControl.SetPowerRight(dx);
            }else if(dx < -0.1){
                MotionControl.SetPowerLeft(Math.Abs(dx));
            }
        }else if(dy < -0.1){
            MotionControl.Backward(abs);
            if(dx > 0.1){
                MotionControl.SetPowerRight(dx);
            }else if(dx < -0.1){
                MotionControl.SetPowerLeft(Math.Abs(dx));
            }
        }else{
            if(dx > 0.1){
                MotionControl.SetPowerLeft(abs);
            }else if(dx < -0.1){
                MotionControl.SetPowerRight(abs);
            }else{
                MotionControl.Stop();
            }
        }
    }

}