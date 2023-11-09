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
class GridBot {
    private AlphaBot Gunnar;
    private static int NumRows;
    private static int NumCols;
    private int PosX;
    private int PosY;
    private string Heading = "";

    public GridBot(int Rows, int Cols) {
        Gunnar = new AlphaBot(0.4, true);
        NumRows = Rows;
        NumCols = Cols;
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
            Gunnar.LineFollow();
            PosX = tempX; 
            PosY = tempY; 

            int[] SensorValues = Gunnar.TRSensor.ReadLine();

            while(SensorValues.Sum() >= 3){
                Gunnar.MotionControl.Forward(0.1);
                SensorValues = Gunnar.TRSensor.ReadLine();
            }

            Thread.Sleep(100);
            Gunnar.MotionControl.Stop();
        } else {
            Console.WriteLine("Invalid move :)");
        }
    }

    public void Left() {
        Gunnar.TurnLeft();
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
        Gunnar.TurnRight();
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

    private bool PositionValid(int X, int Y)
    {
        return !(X < 0 || X >= NumRows || Y < 0 || Y >= NumCols);
    }

    public void CleanUp() {
        Gunnar.CleanUp();
    }
}