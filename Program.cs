class Program {
    static async Task Main(string[] args) {
        double Power = 0.4;
        bool Calibrate = true;
        int Rows = 5;
        int Cols = 5;

        GridBot Gunnar = new(Power, Calibrate, Rows, Cols);
        TxtParser TParser = new();

        await TxtParser.RunFile("robot.txt", Gunnar);
 
        Gunnar.CleanUp();
    }
}
