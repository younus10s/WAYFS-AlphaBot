class CommandParser {
    private GridBot Gunnar; 

    public CommandParser(GridBot GridBot) {
        Gunnar = GridBot; 
    }

    public void RunCommands(string commands) {

        string[] Parts = commands.Split(',');

        int i = 0;

        // Default place - change this later
        Gunnar.Place(0,0,"NORTH");
        
        while (Parts[i] != null) {
             Execute(Parts[i]);
             i++;
        }

        Gunnar.Report();
            
    }
    
    private void Execute(string Command) {
        //string[] Parts = Command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

        switch(Command){
            //case "PLACE":
                // if (Parts.Length == 4){
                //     Gunnar.Place(int.Parse(Parts[1]), int.Parse(Parts[2]), Parts[3].ToLower());
                // }else{
                //     Console.WriteLine("Invalid Command PLACE");
                // }
                
            //    break;
            case "MOVE":
                Gunnar.Move();
            break;
            case "LEFT":
                Gunnar.Left();
            break;
            case "RIGHT":
                Gunnar.Right();
                break;
            // case "REPORT":
            //     Gunnar.Report();
            //     break;
            default:
                Console.WriteLine("Invalid Command OTHER");
            break;
        }
    }
}