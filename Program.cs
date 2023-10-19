using System.Threading;

class Program
{

    static void Main(string[] args)
    {

        try
        {
            AlphaBot ab = new AlphaBot();
            
            ab.Forward();
            
            Console.Write("Moving! :D\n");

            Thread.Sleep(1000);

            ab.Stop();

        }
        catch(Exception e)
        {
            Console.Write("Ajjjdåååå D:" + e.Message);
        }
        
        Console.Write("Done! \n");  
    }
}
