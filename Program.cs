using System.Threading;

class Program
{

    static void Main(string[] args)
    {
        try
        {
            AlphaBot ab = new AlphaBot();
            
            ab.movement.Forward();
            Console.Write("Moving Forward! :D\n");
            Thread.Sleep(1000);

            //ab. Left();
            //Console.Write("Left! :D\n"); 
            //Thread.Sleep(1000);

            //ab.Backward();
            //Console.Write("Backward! :D\n");
            //Thread.Sleep(1000);

            ab.movement.Right(); 
            Console.Write("Right! :D\n");
            Thread.Sleep(1000);

            ab.movement.Stop();

            Console.Write("Stop! :D\n");
        }
        catch(Exception e)
        {
            Console.Write("Ajjjdåååå D: " + e.Message);
        }
        
        Console.Write("Done! \n");  
    }
}
