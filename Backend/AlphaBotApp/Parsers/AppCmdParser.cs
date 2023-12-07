/* Class CommandParser
 * Parser used for extracting and executing commands from frontend (an array of strings).
 * 
 * RunCommands(string commands)
 * Extract and execute the Commands found in array of strings.
 * Commands:
 *   Place X,Y,Direction
 *   Move
 *   Left
 *   Right
 *   Report
 * 
 * ExecuteCommand() 
 * Find words and run the corresponding method for GridBot.  
 */
public class AppCmdParser
{
    public IRobotCommands Gunnar;

    public AppCmdParser(IRobotCommands robot)
    {
        Gunnar = robot;
    }

    public async Task RunCommand(string command)
    {

        string[] Parts = command.Split(',');

        switch (Parts[0])
        {
            default:
                Console.WriteLine("Invalid Command OTHER");
                break;
            case "PLACE":
                Gunnar.Place(int.Parse(Parts[1]), int.Parse(Parts[2]), Parts[3].ToLower());
                break;
            case "MOVE":
                Gunnar.Move();
                break;
            case "LEFT":
                Gunnar.Left();
                break;
            case "RIGHT":
                Gunnar.Right();
                break;
            case "REPORT":
                await Gunnar.Report();
                break;
        }
    }
}