
class Program
{
    static void Main(string[] args)
    {
        
        Console.Write("Add some numbers! :D\n");

        adder adder = new adder();
        
        Console.Write("First number:\n");
        int a = int.Parse(Console.ReadLine());
        Console.Write("Second number:\n");
        int b = int.Parse(Console.ReadLine());

        int res = adder.add(a, b);

        Console.Write("Is you number " + res.ToString() + "\n");

    }
}
