using Microsoft.Net.Http.Headers;
using MMALSharp.Processors.Motion;

public class FreeBot : AlphaBot
{
    public FreeBot(double power, bool Calibrate) : base(power, Calibrate)
    {
        //Maybe add more variable ints
    }

    public void Move(double dx_, double dy_){
        /*
        double Abs = Math.Sqrt(dx * dx + dy * dy);
        double Ang = Math.Atan2(dy, dx);

        double Scaled_abs = Math.Min(Abs, 0.5);

        double[] powers = CalculatePower(Scaled_abs, Ang);

        if(dy > 0)
            MotionControl.Forward(Scaled_abs);   
        else if(dy < 0)
            MotionControl.Backward(Scaled_abs);

        MotionControl.SetPowerLeft(powers[0]);
        MotionControl.SetPowerRight(powers[1]);
        */

        
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


    public double[] CalculatePower(double abs, double angle){
        double[] powers = new double[2];

        powers[0] = abs * (1 + Math.Cos(angle));
        powers[1] = abs * (1 - Math.Cos(angle));

        //Normalize motor powers if they exceed 1
        double max_power = Math.Max(powers[0], powers[1]);
        max_power = Math.Max(max_power, 1);
        powers[0] /= max_power;
        powers[1] /= max_power;

        return powers;
    }

}