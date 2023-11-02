class Program
{

    static void Main(string[] args)
    {

        GridBot GridBot = new GridBot(5, 5);
        string filePath = "robot.txt";
        if (File.Exists(filePath))
        {
            // Open the text file using a stream reader
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;

                // Read the stream to a string, and write the string to the console
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    Execute(GridBot, line);
                }
            }
        }
        else
        {
            Console.WriteLine("The file does not exist.");
        }

        GridBot.CleanUp();

        // GridBot.Place(2,1,"north");
        // Console.WriteLine("Forwards");
        // GridBot.Move();
        // GridBot.Report(); 
        // Console.WriteLine("Left");
        // GridBot.Left();
        // Console.WriteLine("Forwards");
        // GridBot.Move();
        // GridBot.Report(); 
        // Console.WriteLine("Right");
        // GridBot.Right();
        // Console.WriteLine("Forwards");
        // GridBot.Move();
        // GridBot.Report(); 

        //GridBot.CleanUp();
    }


    static void Execute(GridBot GridBot, string command){
        string[] parts = command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        if(parts.Length == 4 && parts[0] == "PLACE"){
            Gridbot.Place(int.Parse(parts[1]), int.Parse(parts[2]), parts[3]);
            Console.WriteLine($"Place x={parts[1]}, y={parts[2]}, Facing={parts[3]}");
        }else{
            switch(parts[0]){
                case "MOVE":
                    GridBot.Move();
                    Console.WriteLine("Move");
                break;
                case "LEFT":
                    GridBot.Left();
                    Console.WriteLine("Left");
                break;
                case "RIGHT":
                    GridBot.Right();
                    Console.WriteLine("Right");
                    break;
                case "REPORT":
                    GridBot.Report();
                    Console.WriteLine("Report");
                    break;
                default:
                    Console.WriteLine("Invalid command");
                break;
            }
        }
    }
}
