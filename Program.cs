namespace ConsoleApplication
{
class Program
{
    //public static WebSocketHandler webSocketHandler = new WebSocketHandler();

        static async Task Main(string[] args)
        {
            GridBot Gunnar = new GridBot(5, 5);
            TxtParser TParser = new TxtParser(Gunnar);
            TParser.RunFile("robot.txt");

            // Gunnar.Place(2,1,"north");
            // Console.WriteLine("Forwards");
            // Gunnar.Move();
            // Gunnar.Report(); 
            // Console.WriteLine("Left");
            // Gunnar.Left();
            // Console.WriteLine("Forwards");
            // Gunnar.Move();
            // Gunnar.Report(); 
            // Console.WriteLine("Right");
            // Gunnar.Right();
            // Console.WriteLine("Forwards");
            // Gunnar.Move();
            // Gunnar.Report(); 

            App app = new App();

            await App.StartApp(args);

            Gunnar.CleanUp();


        }

        
    
}

}