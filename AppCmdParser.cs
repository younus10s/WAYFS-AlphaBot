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


    public async Task RunCommand(string command) {

        string[] Parts = command.Split(',');
        
        if(Parts[0] == "PLACE")
            Gunnar.Place(int.Parse(Parts[1]), int.Parse(Parts[2]), Parts[3].ToLower());
        else if(Parts[0] == "MOVE")
            Gunnar.Move();
        else if(Parts[0] == "LEFT")
            Gunnar.Left();
        else if(Parts[0] == "RIGHT")
            Gunnar.Right();
        else if(Parts[0] == "REPORT")
            await Gunnar.Report();
        else 
            Console.WriteLine("Invalid Command OTHER");
    }
    
}