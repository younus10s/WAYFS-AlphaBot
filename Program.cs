class Program {
    static async Task Main(string[] args) {
        
        GridBot Gunnar = new GridBot(5, 5);
        TxtParser TParser = new TxtParser(Gunnar);
        TParser.RunFile("robot.txt");
        await Gunnar.Gunnar.Cam.takePicture();
        TParser.RunFile("robot.txt");
 
        // Gunnar.Place(2,1,"north");
        // Console.WriteLine("Forwards");
        // Gunnar.Move();
        // Gunnar.Report(); 
        // Console.WriteLine("Left");
        // Gunnar.Left();

        Gunnar.CleanUp();
        
        //Ab.Cam.RunCamera();
        //Console.WriteLine("Funkar :)"); 

    }
}
