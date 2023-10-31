using System.Threading;

class Program
{

    static void Main(string[] args)
    {
        using (AlphaBot ab = new AlphaBot())
        {
            bool Follow = true;

            while(Follow){
                Follow = ab.LineFollow();
            }

            ab.MotionControl.Stop();

            Console.Write("Stop! D:\n");
        }
    }
}
