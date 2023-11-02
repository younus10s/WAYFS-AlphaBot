using Microsoft.AspNetCore.Components;

class GridBot
{
    private AlphaBot Gunnar;

    private static int NumRows;
    private static int NumCols;

    private int PosX;
    private int PosY;
    private string Facing;

    public GridBot(int rows, int cols)
    {
        Gunnar = new AlphaBot(0.2);
        NumRows = rows;
        NumCols = cols;
    }

    public void Place(int x, int y, string facing)
    {
        PosX = x;
        PosY = y;
        Facing = facing;
    }

    private bool PositionValid(int x, int y) 
    {
        return !(x<0 || x>=NumRows || y<0 || y>=NumCols); 
    }

    public void Move()
    {
        int tempX = PosX;
        int tempY = PosY; 

        switch(Facing) {
            case "north": 
                tempY+=1; 
                break;
            case "east":
                tempX+=1; 
                break; 
            case "south": 
                tempY-=1;
                break;
            case "west":
                tempX-=1; 
                break; 
        }
        
        if (PositionValid(tempX, tempY)) 
        {
            if (Gunnar.LineFollow()) {
                PosX = tempX; 
                PosY = tempY; 

                int[] SensorValues = Gunnar.Trsensor.ReadLine();

                while(SensorValues.Sum() >= 3){
                    Gunnar.MotionControl.Forward(0.1);
                    SensorValues = Gunnar.Trsensor.ReadLine();
                }

                Thread.Sleep(100);
                Gunnar.MotionControl.Stop();            
            }
            else {
                Console.WriteLine("Failed LineFollow() :((((((");
            }
        }
        else 
        {
            Console.WriteLine("Invalid position!");
        }
    }

    public void Left()
    {
        Gunnar.TurnLeft();
        switch(Facing) {
            case "north": 
                Facing="west"; 
                break;
            case "west":
                Facing="south"; 
                break; 
            case "south": 
                Facing="east";
                break;
            case "east":
                Facing="north"; 
                break; 
        }
    }

    public void Right()
    {
        Gunnar.TurnRight();
        switch(Facing) {
            case "north": 
                Facing="east"; 
                break;
            case "east":
                Facing="south"; 
                break; 
            case "south": 
                Facing="west";
                break;
            case "west":
                Facing="north"; 
                break; 
        }
    }

    public void Report()
    {
        Console.WriteLine("pos: (" + PosX + "," + PosY + ") facing: " + Facing);
    }

    public void RunFile(string filePath)  {
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
                    Execute(line);
                }
            }
        }
        else
        {
            Console.WriteLine("The file does not exist.");
        }
    }
    

    private void Execute(string command){
        string[] parts = command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        if(parts.Length == 4 && parts[0] == "PLACE"){
            Place(int.Parse(parts[1]), int.Parse(parts[2]), parts[3]);
            Report();
        }else{
            switch(parts[0]){
                case "MOVE":
                    Move();
                    Console.WriteLine("Move");
                    Console.WriteLine("PosX: " + PosX);
                    Console.WriteLine("PosY: " + PosY);
                break;
                case "LEFT":
                    Left();
                    Console.WriteLine("Left");
                break;
                case "RIGHT":
                    Right();
                    Console.WriteLine("Right");
                    break;
                case "REPORT":
                    Report();
                    Console.WriteLine("Report");
                    break;
                default:
                    Console.WriteLine("Invalid command");
                break;
            }
        }
    }

    public void CleanUp(){
        Gunnar.CleanUp();
    }
}