class Program
{
    static void Main(string[] args)
    {
        // 1. Initialize your cable object
        Cable cable = new();

        // 2. Solve the physics
        Console.WriteLine("Solving wave equation...");
        List<double[]> u = Method.SolveWave(cable);

        // 3. Define path (Using .csv extension for easy opening in Excel/Python)
        string filepath = Path.Combine(Environment.CurrentDirectory, "data.csv");

        // 4. Export
        Method.ToFile(u, filepath);

        Console.WriteLine("Wrote file to " + filepath);
    }
}