using Microsoft.Net.Http.Headers;
using MMALSharp.Processors.Motion;

public class FreeBot : AlphaBot
{
    public FreeBot(double power, bool Calibrate) : base(power, Calibrate)
    {
        //Maybe add more variable ints
    }

    public void Move(double dx_, double dy_){
        double dx = dx_ * 0.80;
        double dy = dy_ * 0.80;
        double abs = Math.Sqrt(dx * dx + dy * dy);
        if(dy > 0.2){
            MotionControl.Forward(abs);
            if(dx > 0.2){
                MotionControl.SetPowerRight(dx * 0.7);
            }else if(dx < -0.2){
                MotionControl.SetPowerLeft(Math.Abs(dx) * 0.7);
            }
        }else if(dy < -0.2){
            MotionControl.Backward(abs);
            if(dx > 0.2){
                MotionControl.SetPowerRight(Math.Abs(dx) * 0.7); 
            }else if(dx < -0.2){
                MotionControl.SetPowerLeft(dx);
            }
        }else{
            if(dx > 0.2){
                MotionControl.SetPowerLeft(abs*0.7);
            }else if(dx < -0.2){
                MotionControl.SetPowerRight(abs* 0.7);
            }else{
                MotionControl.Stop();
            }
        }
    }

}