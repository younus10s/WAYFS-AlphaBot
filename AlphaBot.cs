class AlphaBot : IDisposable
{
    public MotionControl MotionControl = new MotionControl();
    public TRSensor Trsensor = new TRSensor();

    public bool LineFollow(){
        double power = 0.8;
        int[] SensorValues = Trsensor.ReadLine(Trsensor.AnalogRead());
        MotionControl.Stop();
        MotionControl.Forward(power);

        if(SensorValues.Sum() >= 3 || SensorValues.Sum() == 0){
            Console.WriteLine("JUNCTION. STOPPING...");
	        MotionControl.Forward(0);
            return false;
	    }

        int[] forward = {0,0,1,0,0};
        int[] left1 = {0,0,1,1,0};
        int[] left2 = {0,0,0,1,0};
        int[] right1 = {0,1,1,0,0};
        int[] right2 = {0,1,0,0,0};

        if(SensorValues.SequenceEqual(forward)){
            MotionControl.Forward(power);
        }
        else if(SensorValues.SequenceEqual(left1) || SensorValues.SequenceEqual(left2)){
            MotionControl.SetPowerRight(power*0.9);
        } 
        else if(SensorValues.SequenceEqual(right1) || SensorValues.SequenceEqual(right2)){
	        MotionControl.SetPowerLeft(power*0.9);
        }else{
            Console.WriteLine("Unhandeled case");
            return false;
        }

        return true;
    }

    public void Dispose(){
        MotionControl.CleanUp();
    }
}
