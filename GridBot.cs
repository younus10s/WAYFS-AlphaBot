using Microsoft.AspNetCore.Components;

class GridBot
{
    private AlphaBot Gunnar;

    private static int NumRows;
    private static int NumCols;

    private int PosX;
    private int PosY;
    private int Facing;

    public GridBot(int rows, int cols)
    {
        Gunnar = new AlphaBot();
        NumRows = rows;
        NumCols = cols;
    }

    public void Place(int x, int y, int facing)
    {
        PosX = x;
        PosY = y;
        Facing = facing;
    }

    public void Move()
    {
        Gunnar.LineFollow();

        int[] SensorValues = Gunnar.Trsensor.ReadLine();

        while(SensorValues.Sum() >= 3){
            Gunnar.MotionControl.Forward(0.1);
            SensorValues = Gunnar.Trsensor.ReadLine();
        }
        
        Gunnar.MotionControl.Stop();
    }

    public void Left()
    {
        Gunnar.TurnLeft();
    }

    public void Right()
    {
        Gunnar.TurnRight();
    }

    public void Report()
    {
        //Raportera state
    }

    public void CleanUp(){
        Gunnar.CleanUp();
    }
}