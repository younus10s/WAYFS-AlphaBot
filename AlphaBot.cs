class AlphaBot
{
    public MotionControl MotionControl = new MotionControl();
    public TRSensor Trsensor = new TRSensor();
    private double power;

    public AlphaBot(double power_){
        power = power_;
    }


    public void TurnLeft(){
        int[] SensorValues = Trsensor.ReadLine();

        MotionControl.Left(power);

        while(SensorValues[2]==1){
            SensorValues = Trsensor.ReadLine();
        }

        while(SensorValues[2]==0){
            SensorValues = Trsensor.ReadLine();
        }

        //Thread.Sleep(100);
        MotionControl.Stop();
    }

    public void TurnRight(){
        int[] SensorValues = Trsensor.ReadLine();

        MotionControl.Right(power);
        
        while(SensorValues[2]==1){
            SensorValues = Trsensor.ReadLine();
        }

        while(SensorValues[2]==0){
            SensorValues = Trsensor.ReadLine();
        }

        //Thread.Sleep(100);
        MotionControl.Stop();
    }

    public bool LineFollow(){
        int[] SensorValues;
    
        int[] forward = {0,0,1,0,0};
        int[] left1 =   {0,0,1,1,0};
        int[] left2 =   {0,0,0,1,0};
        int[] right1 =  {0,1,1,0,0};
        int[] right2 =  {0,1,0,0,0};

        MotionControl.Forward(power);

        bool Continue = true;

        while(Continue){
            SensorValues = Trsensor.ReadLine();

            if(SensorValues.Sum() >= 3){
                MotionControl.Stop();
                Continue = false;
            }
            else if(SensorValues.SequenceEqual(forward)){
                MotionControl.Forward(power);
            }
            else if(SensorValues.SequenceEqual(left1) || SensorValues.SequenceEqual(left2) || SensorValues[4]==1){
                MotionControl.SetPowerRight(power*0.9);
            } 
            else if(SensorValues.SequenceEqual(right1) || SensorValues.SequenceEqual(right2) || SensorValues[0]==1){
                MotionControl.SetPowerLeft(power*0.9);
            }else{
                Console.WriteLine("Unhandeled case");

                string printSensor = string.Join(", ", SensorValues);
                Console.WriteLine("SensonValues: " + printSensor);

                Continue = false;
                return false; 
            }
        }
        return true; 
    }

    public void CleanUp(){
        MotionControl.CleanUp();
    }
}
