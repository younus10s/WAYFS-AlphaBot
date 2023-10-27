class AlphaBot : IDisposable
{
    public Movement movement = new Movement();
    public TRSensor Trsensor = new TRSensor();

    public void Dispose(){
        movement.CleanUp();
    }
}
