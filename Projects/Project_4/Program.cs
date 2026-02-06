class Program
{
    static void Main(String[] args)
    {   
        // Time the render
        // Stopwatch sw = new();

        // Initialize the medium
        Medium3D medium = new(-0.5, 0.5, -0.5, 0.5, -0.5, 0.5, 1.0/40.0);

        // Add a potential disk
        medium.AddDisk(0, 0, 0, 0.1, 0.25);

        // Add point charges
        medium.AddCharge( 0.25,  0.00, 0.00,  1e-6);
        medium.AddCharge( 0.00,  0.25, 0.00,  1e-6);
        medium.AddCharge(-0.25,  0.00, 0.00, -1e-6);
        medium.AddCharge( 0.00, -0.25, 0.00, -1e-6);

        // Solve the potential
        Methods.SolveSOR(medium, 1.0);

        // Store the filepath in a string
        string filepath = Path.Combine(Environment.CurrentDirectory, "data.csv");

        // Output to a file
        Methods.ExportCSV(medium, filepath);
    }
}