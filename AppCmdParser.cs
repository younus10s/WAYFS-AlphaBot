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

public class AppCmdParser {
    public GridBot Gunnar; 

    public AppCmdParser(GridBot gridBot) {
        Gunnar = gridBot;
    }

    public void RunCommands(string commands) {

        string[] Parts = commands.Split(',');

        int i = 0;

        while (Parts[i] != null) {
             ExecuteCommand(Parts[i]);
             Console.WriteLine($"Command exectued was: {Parts[i]} \n");
             i++;
        }
    }
    
    private void ExecuteCommand(string Command) {
        //string[] Parts = Command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

        switch(Command){
            case "PLACE":
                // if (Parts.Length == 4){
                //     Gunnar.Place(int.Parse(Parts[1]), int.Parse(Parts[2]), Parts[3].ToLower());
                // }else{
                //     Console.WriteLine("Invalid Command PLACE");
                // }
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
                Gunnar.Report();
                break;
            default:
                Console.WriteLine("Invalid Command OTHER");
            break;
        }
    }
}