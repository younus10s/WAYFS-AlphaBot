class AlphaBot : IDisposable
{
    public Movement movement = new Movement();
    public TRSensor Trsensor = new TRSensor();

    public void LineFollow(){
        double power = 0.5;
        int[] blackLines = Trsensor.ReadLine(Trsensor.AnalogRead());
        movement.Stop();
        movement.Forward(power);

        // if there is four ones or all zeros => stop
        // if ir1 is one => rightmotor more power
        // if ir5 is ones => leftmoter more power
        
        if(blackLines.Sum() >= 4 || blackLines.Sum() == 0)
            Console.WriteLine("Cross!!!!");
        else if(blackLines[0] == 1){
            Console.WriteLine("TurnLeft");
        }else if(blackLines[4] == 1){
            Console.WriteLine("TurnRight");
        }else{
            //Återställa power
        }
        

    }

    public void Dispose(){
        movement.CleanUp();
    }
}
