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
            string command = Parts[i];
            switch(command){
                case "PLACE":
                    //if (Parts[1].GetType() == typeof(int) && Parts[2].GetType() == typeof(int) && Parts[3].GetType() == typeof(string)){
                        Gunnar.Place(int.Parse(Parts[1]), int.Parse(Parts[2]), Parts[3].ToLower());
                        Console.WriteLine($"Place command exectued was: {Parts[1]}, {Parts[2]}, {Parts[3]}\n");
                        i = 3; // Jump array to index 3 as X,Y,Direction has been used
                    //}else{
                    //    Console.WriteLine("Invalid Command PLACE");
                    //}
                    break;
                case "MOVE":
                // Send a string message back to the client

                    Gunnar.Move();
                    
                   // Gunnar.SendMessageToClient("Move");

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
                Console.WriteLine($"Command exectued was: {command} \n");
                i++;
            }

                Console.WriteLine($"Number of commands exectued was: {i} \n");
    }
    
}