/* This interface specifies the different commands that a Robot may recieve. */

public interface IRobotCommands
{
    int PosX { get; set; }
    int PosY { get; set; }
    string Heading { get; set; }

    void Place(int x, int y, string heading);
    void Move();
    void Left();
    void Right();
    Task Report();
}