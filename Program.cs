class Program
{

    static void Main(string[] args)
    {
        GridBot GridBot = new GridBot(5, 5);
        GridBot.Place(2,1,"north");
        Console.WriteLine("Forwards");
        GridBot.Move();
        GridBot.Report(); 
        Console.WriteLine("Left");
        GridBot.Left();
        Console.WriteLine("Forwards");
        GridBot.Move();
        GridBot.Report(); 
        Console.WriteLine("Right");
        GridBot.Right();
        Console.WriteLine("Forwards");
        GridBot.Move();
        GridBot.Report(); 
        Console.WriteLine("Left");
        GridBot.Left();
        Console.WriteLine("Forwards");
        GridBot.Move();
        GridBot.Report(); 
        Console.WriteLine("Right");
        GridBot.Right();
        Console.WriteLine("Forwards");
        GridBot.Move();  
        GridBot.Report(); 

        GridBot.CleanUp();
    }
}
