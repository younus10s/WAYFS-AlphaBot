class Program
{

    static void Main(string[] args)
    {
        GridBot GridBot = new GridBot(5, 5);
        Console.WriteLine("Forwards");
        GridBot.Move();
        Console.WriteLine("Left");
        GridBot.Left();
        Console.WriteLine("Forwards");
        GridBot.Move();
        Console.WriteLine("Right");
        GridBot.Right();
        Console.WriteLine("Forwards");
        GridBot.Move();  

        GridBot.CleanUp();
    }
}
