/* Class GridBot
 * Class to Abstract the AlphaBot to a Grid Traversing Robot.
 * Contains logic and functions to satisfy the HiQ gridbot code test 
 * 
 * Place(int x, int y, string Heading)
 * Places the robot with (x,y) coords and a heading ("north", "south", "east", "west") in the grid 
 * 
 * Move() 
 * First checks if moving one step MOVE in the direction it is facing will take the robot out of the grid. 
 * Then calls Alphabot's LineFollow(). 
 * 
 * Left() / Right()
 * Make the robot turns 90 degree on a crossing
 */
using System.Drawing;

public class GridBot : AlphaBot
{
    private static int NumRows;
    private static int NumCols;
    public int PosX;
    public int PosY;
    public string Heading = "";

    public GridBot(double power, bool Calibrate, int Rows, int Cols) : base(power, Calibrate)
    {
        NumRows = Rows;
        NumCols = Cols;
    }

    public void Place(int PosX_, int PosY_, string Heading_)
    {
        PosX = PosX_;
        PosY = PosY_;
        Heading = Heading_;
    }

    public void Move()
    {
        int tempX = PosX;
        int tempY = PosY;

        switch (Heading)
        {
            case "north":
                tempY += 1;
                break;
            case "east":
                tempX += 1;
                break;
            case "south":
                tempY -= 1;
                break;
            case "west":
                tempX -= 1;
                break;
        }

        if (PositionValid(tempX, tempY))
        {
            bool MoveDone = false;
            while (!MoveDone)
            {
                try
                {
                    Lights.ShowAll(Color.Green);

                    LineFollow();
                    MoveDone = true;
                }
                catch (OffLineException e)
                {
                    Lights.ShowAll(Color.Red);

                    Console.WriteLine(MoveDone);
                    MoveDone = false;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Whoops! Put me back on the line please! :D");
                    Console.WriteLine("Press any key when ready!");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Lights.ShowAll(Color.Red);

                    Console.WriteLine(e.Message);

                    CleanUp();
                    Environment.Exit(0);
                }
            }

            PosX = tempX;
            PosY = tempY;

            int[] SensorValues = TRSensor.ReadLine();
            MotionControl.Forward(0.1);

            while (SensorValues.Sum() >= 3)
            {
                SensorValues = TRSensor.ReadLine();
            }

            MotionControl.Stop();
        }
        else
        {
            Console.WriteLine("Invalid move");
        }

        Lights.SwitchOff();
    }

    public void Left()
    {
        Lights.StartBlinking(new int[] { 2, 3 }, Color.Orange);

        TurnLeft();
        switch (Heading)
        {
            case "north":
                Heading = "west";
                break;
            case "west":
                Heading = "south";
                break;
            case "south":
                Heading = "east";
                break;
            case "east":
                Heading = "north";
                break;
        }

        Lights.StopBlinking();
    }

    public void Right()
    {
        Lights.StartBlinking(new int[] { 0, 1 }, Color.Orange);

        TurnRight();
        switch (Heading)
        {
            case "north":
                Heading = "east";
                break;
            case "east":
                Heading = "south";
                break;
            case "south":
                Heading = "west";
                break;
            case "west":
                Heading = "north";
                break;
        }
    }

    public async Task Report()
    {
        Lights.StartColorWipe();
        await TakePicture();
        Console.WriteLine("Report() \tpos: (" + PosX + "," + PosY + ") facing: " + Heading);
    }

    private static bool PositionValid(int X, int Y)
    {
        return !(X < 0 || X >= NumRows || Y < 0 || Y >= NumCols);
    }



    public List<string> FindPath(int destX, int destY)
    {
        int currX = PosX;
        int currY = PosY;

        string dir = Heading;

        List<string> path = new();
        while (true)
        {
            Console.WriteLine("here 1 " + currX + " " + currY);
            // Adjust direction towards destination X
            AdjustX(path, ref currX, ref destX, ref dir);
            Console.WriteLine("here 2 " + currX + " " + currY);

            // Adjust direction towards destination Y
            AdjustY(path, ref currY, ref destY, ref dir);
            Console.WriteLine("here 3 " + currX + " " + currY);

            // Destination reached
            if (currX == destX && currY == destY) 
                break;
        }

        path.Add("REPORT");

        return path;
    }

    private void AdjustX(List<string> path, ref int CurrX, ref int destX, ref string dir){
        while (CurrX != destX)
        {
            if ((CurrX < destX && dir == "EAST") || (CurrX > destX && dir == "WEST"))
            {
                path.Add("MOVE");
                if(dir == "EAST")
                    CurrX++;
                else 
                    CurrX--;
            }
            else
            {
                if (CurrX < destX)
                {
                    if(dir == "SOUTH")
                    {
                        path.Add("LEFT");
                        dir = "EAST";
                    }
                    else
                    {
                        path.Add("RIGHT");
                        dir = "EAST";
                    }
                }
                else
                {
                    if(dir == "NORTH")
                    {
                        path.Add("LEFT");
                        dir = "WEST";
                    }
                    else
                    {
                        path.Add("RIGHT");
                        dir = "WEST";
                    }
                }
            }
        }
    }

    private void AdjustY(List<string> path, ref int CurrY, ref int destY, ref string dir){
        while (CurrY != destY)
        {
            if ((CurrY < destY && dir == "NORTH") || (CurrY > destY && dir == "SOUTH"))
            {
                path.Add("MOVE");
                if(dir == "NORTH")
                    CurrY++;
                else
                    CurrY--;
            }
            else
            {
                if (CurrY < destY)
                {
                    if(dir == "EAST")
                    {
                        path.Add("LEFT");
                        dir = "NORTH";
                    }
                    else
                    {
                        path.Add("RIGHT");
                        dir = "NORTH";
                    }
                }
                else
                {
                    if(dir == "WEST")
                    {
                        path.Add("LEFT");
                        dir = "SOUTH";
                    }
                    else
                    {
                        path.Add("RIGHT");
                        dir = "SOUTH";
                    }
                }
            }
        }
    }
}
