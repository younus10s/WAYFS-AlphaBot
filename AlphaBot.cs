class AlphaBot : IDisposable
{
    public Movement movement = new Movement();  

    public void Dispose(){
        movement.CleanUp();
    }
}