using icantlivewithoutdiscretemath;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine(GraphGenerator.Generate(4, 0.1, true).AdjacencyList);
    }
}