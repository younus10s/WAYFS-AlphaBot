using System.Threading;

class Program
{

    static void Main(string[] args)
    {
        using (AlphaBot ab = new AlphaBot())
        {
            //ab.movement.Forward(1);
            //Console.Write("Moving Forward! :D\n");
            //Thread.Sleep(1000);

            //ab.movement.Stop();
            //Console.Write("Stop! :D\n");
            //Thread.Sleep(1000);

            //ab.movement.Left(0.9);
            //Console.Write("Left! :D\n");
            //Thread.Sleep(1000);

            //ab.movement.Right(0.5);
            //Console.Write("Right! :D\n");
            //Thread.Sleep(1000);

            //ab.movement.Backward(0.5);
            //Console.Write("Back! :D\n");
            //Thread.Sleep(1000);
            // while(true){
            // ab.Trsensor.PrintValues(ab.Trsensor.ReadLine(ab.Trsensor.AnalogRead()));
            // Thread.Sleep(100);
            // }

            bool Follow = true;

            while(Follow){
                Follow = ab.LineFollow();
            }

            ab.MotionControl.Stop();

            Console.Write("Stop! D:\n");
        }
    }
}
