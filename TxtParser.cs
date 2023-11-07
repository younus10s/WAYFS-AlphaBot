/*
 * Text parser, used for extracting and executing commands and parameters from a text file.
 * Call Runfile(filePath) to extract and execute the commands 
 * Commands:
 *  -Place X,Y,Direction
 *  -Move
 *  -Left
 *  -Right
 *  -Report
*/
class TxtParser {
    private GridBot Gunnar; 

    public TxtParser(GridBot gb) {
        Gunnar = gb; 
    }

    public void RunFile(string filePath){
        if (File.Exists(filePath)) {
            // Open the text file using a stream reader
            using (StreamReader sr = new StreamReader(filePath)) {
                string? line;
                // Read the stream to a string, and write the string to the console
                while ((line = sr.ReadLine()) != null) {
                    //Console.WriteLine(line);
                    Execute(line);
                }
            }
        } else {
            Console.WriteLine("The file does not exist.");
        }
    }
    
    private void Execute(string command) {
        string[] parts = command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        if(parts.Length == 4 && parts[0] == "PLACE"){
            Gunnar.Place(int.Parse(parts[1]), int.Parse(parts[2]), parts[3].ToLower());
            Gunnar.Report();
        } else {
            switch(parts[0]){
                case "MOVE":
                    Gunnar.Move();
                    Console.WriteLine("Move");
                break;
                case "LEFT":
                    Gunnar.Left();
                    Console.WriteLine("Left");
                break;
                case "RIGHT":
                    Gunnar.Right();
                    Console.WriteLine("Right");
                    break;
                case "REPORT":
                    Gunnar.Report();
                    Console.WriteLine("Report");
                    break;
                default:
                    Console.WriteLine("Invalid command");
                break;
            }
        }
    }
}