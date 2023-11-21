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
    public static async Task RunFile(string FilePath, GridBot Gunnar){
        if (File.Exists(FilePath)) {

            using StreamReader Reader = new(FilePath);
            string? Line;

            while ((Line = Reader.ReadLine()) != null)
            {
                await Execute(Line, Gunnar);
            }
        } else {
            Console.WriteLine("The file does not exist.");
        }
    }
    
    private static async Task Execute(string Command, GridBot Gunnar) {
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
                Console.WriteLine("MOVE");
            break;
            case "LEFT":
                Gunnar.Left();
                Console.WriteLine("LEFT");
            break;
            case "RIGHT":
                Gunnar.Right();
                Console.WriteLine("RIGHT");
                break;
            case "REPORT":
                Gunnar.Report();
                break;
            case "IMAGE":
                await Gunnar.TakePicture();
                break;
            default:
                Console.WriteLine("Invalid Command OTHER");
            break;
        }
    }
}