/* Class GridBot
 * Class to Abstract the AlphaBot to a Grid Traversing Robot.
 * Contains logic and functions to satisfy the HiQ gridbot code test 
 * 
 * Place(int x, int y, string Heading)
 * Places the robot with (x,y) coords and a heading ("north", "south", "east", "west") in the grid 
 * 
 * Move() 
 * First checks if moving one step forward in the direction it is facing will take the robot out of the grid. 
 * Then calls Alphabot's LineFollow(). 
 * 
 * Left() /Right()
 * Make the robot turns 90 degree on a crossing
 */
public class GridBot {
    private AlphaBot AlphaBot;
    private static int NumRows;
    private static int NumCols;
    private int PosX;
    private int PosY;
    private string Heading = "";

    public GridBot(int Rows, int Cols) {
        AlphaBot = new AlphaBot(0.4, true);
        NumRows = Rows;
        NumCols = Cols;
    }

    public async Task TakePicture(){
        await AlphaBot.Camera.TakePicture();
    }

    public void Place(int PosX_, int PosY_, string Heading_) {
        PosX = PosX_;
        PosY = PosY_;
        Heading = Heading_;
    }

    public void Move() {
        int tempX = PosX;
        int tempY = PosY; 

        switch(Heading) {
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
        
        if(PositionValid(tempX, tempY)) {
            bool MoveDone = false;
            while (!MoveDone)
            {
                try
                {
                    AlphaBot.LineFollow();
                    MoveDone = true;
                }
                catch (OffLineException e)
                {
                    Console.WriteLine(MoveDone);
                    MoveDone = false; 
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Whoops! Put me back on the line please! :D");
                    Console.WriteLine("Press any key when ready!");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    CleanUp();
                    Environment.Exit(0);
                }                
            }

            PosX = tempX; 
            PosY = tempY; 

            int[] SensorValues = AlphaBot.TRSensor.ReadLine();
            AlphaBot.MotionControl.Forward(0.1);

            while(SensorValues.Sum() >= 3){
                SensorValues = AlphaBot.TRSensor.ReadLine();
            }

            AlphaBot.MotionControl.Stop();
        } else {
            Console.WriteLine("Invalid move :)");
        }
    }

    public void Left() {
        AlphaBot.TurnLeft();
        switch(Heading) {
            case "north": 
                Heading="west"; 
                break;
            case "west":
                Heading="south"; 
                break; 
            case "south": 
                Heading="east";
                break;
            case "east":
                Heading="north"; 
                break; 
        }
    }

    public void Right() {
        AlphaBot.TurnRight();
        switch(Heading) {
            case "north": 
                Heading="east"; 
                break;
            case "east":
                Heading="south"; 
                break; 
            case "south": 
                Heading="west";
                break;
            case "west":
                Heading="north"; 
                break; 
        }
    }

    public void Report(){
        Console.WriteLine("Report() \tpos: (" + PosX + "," + PosY + ") facing: " + Heading);
    }

    public void CleanUp() {
        AlphaBot.CleanUp();
    }
 
    private bool PositionValid(int X, int Y)
    {
        return !(X < 0 || X >= NumRows || Y < 0 || Y >= NumCols);
    }
}
