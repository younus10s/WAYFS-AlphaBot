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

class TxtParser {
    private GridBot Gunnar; 

    public TxtParser(GridBot GridBot) {
        Gunnar = GridBot; 
    }

    public void RunFile(string FilePath){
        if (File.Exists(FilePath)) {

            using (StreamReader Reader = new StreamReader(FilePath)) {
                string? Line;

                while ((Line = Reader.ReadLine()) != null) {
                    Execute(Line);
                }
            }
        } else {
            Console.WriteLine("The file does not exist.");
        }
    }
    
    private void Execute(string Command) {
        string[] Parts = Command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

        switch(Parts[0]){
            case "PLACE":
                if (Parts.Length == 4){
                    Gunnar.Place(int.Parse(Parts[1]), int.Parse(Parts[2]), Parts[3].ToLower());
                }else{
                    Console.WriteLine("Invalid Command PLACE");
                }
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