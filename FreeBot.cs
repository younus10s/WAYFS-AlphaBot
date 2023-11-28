using MMALSharp.Processors.Motion;

public class FreeBot : AlphaBot
{
    public FreeBot(double power, bool Calibrate) : base(power, Calibrate)
    {
        //Maybe add more variable ints
    }

    public void Move(double dx, double dy){
        double AbsDy = Math.Abs(dy);
        double AbsDx = Math.Abs(dx);
        
        //Controll movement
        if(dy > 0)
        {
            MotionControl.Forward(AbsDy);
            //Controll rotation
            if(dx > 0)
            {
                Console.WriteLine($"Move forward with {AbsDy} and and Rotate clockwise with {AbsDx}");
                MotionControl.SetPowerLeft(AbsDy);
                MotionControl.SetPowerRight(0.25);
            }
            else if(dx < 0)
            {
                Console.WriteLine($"Move forward with {AbsDy} and Rotate anti-clockwise with {AbsDx}");
                MotionControl.SetPowerLeft(0.25);
                MotionControl.SetPowerRight(AbsDy);
            }
            else
            {
                Console.WriteLine($"Move forward with {AbsDy}");
            }  
        }
        else if(dy < 0)
        {
            
            MotionControl.Backward(AbsDy);

            if(dx > 0)
            {
                Console.WriteLine($"Move backward with {AbsDy} and and Rotate clockwise with {AbsDx}");
                MotionControl.SetPowerLeft(AbsDy);
                MotionControl.SetPowerRight(0.25);
            }
            else if(dx < 0)
            {
                Console.WriteLine($"Move backward with {AbsDy} and Rotate anti-clockwise with {AbsDx}");
                MotionControl.SetPowerLeft(0.25);
                MotionControl.SetPowerRight(AbsDy);
            }
            else
            {
                Console.WriteLine($"Move backward with {AbsDy}");
            }  
        }
        else
        {
            Console.WriteLine("Stop Movement");
            MotionControl.Stop();
        }
        
    }

}