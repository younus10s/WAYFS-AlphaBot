using System.Drawing;

public class FreeBot : AlphaBot
{
    public FreeBot(double power, bool Calibrate) : base(power, Calibrate)
    {
        //Maybe add more variable ints
    }

    public void Move(double dx, double dy)
    {    
        double Abs = Math.Sqrt(dx * dx + dy * dy);
        double Ang = Math.Atan2(dy, dx);

        double Scaled_abs = Math.Min(Abs, 0.5);

        double[] powers = CalculatePower(Scaled_abs, Ang);

        if(dy >= 0)
            MotionControl.ActivateForward();   
        else if(dy < -0)
            MotionControl.ActivateBackward();

        MotionControl.SetPowerLeft(powers[0] * 0.6);
        MotionControl.SetPowerRight(powers[1]* 0.6);

        if(dy > 0.1)
            Lights.Show(Color.Green);

        else if (dy < -0.1)
            Lights.Show(Color.Red);   
        else
            Lights.Show(Color.Yellow);
        
    }

    public double[] CalculatePower(double abs, double angle)
    {
        double[] powers = { abs * (1 + Math.Cos(angle)), abs * (1 - Math.Cos(angle)) };

        //Normalize motor powers if they exceed 1
        double max_power = Math.Max(powers[0], powers[1]);
        max_power = Math.Max(max_power, 1);
        powers[0] /= max_power;
        powers[1] /= max_power;

        return powers;
    }
}