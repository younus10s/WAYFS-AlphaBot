/* Class TxtParser
 * Parser used for extracting and executing commands from a text file.
 * 
 * Runfile(string FilePath)
 * Extract and execute the Commands found in .txt file FilePath.
 * Commands for file in FilePath:
 *   Place X,Y,Direction
 *   Move
 *   Left
 *   Right
 *   Report
 * 
 * Execute() 
 * Find words and run the corresponding method for GridBot.  
 */

class CommandParser {
    private GridBot Gunnar; 

    public CommandParser(GridBot GridBot) {
        Gunnar = GridBot; 
    }

    public void RunCommands(string commands) {

        string[] Parts = commands.Split(',');

        int i = 0;

        while (Parts[i] != null) {
             Execute(Parts[i]);
             i++;
        }
            
    }
    
    private void Execute(string Command) {
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