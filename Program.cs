class Program {
    static async Task Main(string[] args) {
        
        GridBot Gunnar = new GridBot(5, 5);
        TxtParser TParser = new TxtParser();

        TParser.RunFile("robot.txt", Gunnar);

        await Gunnar.TakePicture();

        TParser.RunFile("robot.txt", Gunnar);
 
        Gunnar.CleanUp();
    }
}
