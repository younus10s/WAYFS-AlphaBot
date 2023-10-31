class Program
{

    static void Main(string[] args)
    {
        GridBot GridBot = new GridBot(5, 5);
        GridBot.Move();

        GridBot.CleanUp();
    }
}
