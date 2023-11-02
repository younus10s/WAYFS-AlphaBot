class Program
{
    static void Main(string[] args)
    {
        GridBot gunnar = new GridBot(5, 5);
        //gunnar.RunFile("robot.txt");

        // gunnar.Place(2,1,"north");
        // Console.WriteLine("Forwards");
        // gunnar.Move();
        // gunnar.Report(); 
        // Console.WriteLine("Left");
        // gunnar.Left();
        // Console.WriteLine("Forwards");
        // gunnar.Move();
        // gunnar.Report(); 
        // Console.WriteLine("Right");
        // GridBot.Right();
        // Console.WriteLine("Forwards");
        // GridBot.Move();
        // GridBot.Report(); 

        gunnar.CleanUp();
    }
}
