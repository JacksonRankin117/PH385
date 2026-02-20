using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {   
        // Time the render
        Stopwatch sw = new();
        sw.Start();

        // Initialize a cube object with bounds
        Cube cube = new(-0.5, 0.5,   // x-lims
                        -0.5, 0.5,   // y-lims
                        -0.5, 0.5);  // z-lims

        // Populate the cube with 10_000 particles at the origin
        List<Particle> particles = cube.Populate(10_000, 0.025);

        // Find the filepath
        string pos_filepath = Path.Combine(Environment.CurrentDirectory, "pos.csv");
        string rms_filepath = Path.Combine(Environment.CurrentDirectory, "rms.csv");

        // Initiate random walk.
        cube.Diffuse(particles, pos_filepath, rms_filepath);

        // Record the elapsed time
        TimeSpan elapsed = sw.Elapsed;

        // Write the duration of the sim to the console
        Console.WriteLine($"Time elapsed: {elapsed.TotalMilliseconds} ms");
    }
}