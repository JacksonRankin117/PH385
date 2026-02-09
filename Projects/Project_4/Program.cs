/*  Programmer: Jackson Rankin
 *        Date: February 1st, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This is the main file for my Project 4 submission for PH 385, Numerical Modeling Physics. This 
 *              submission presents no change in style or idealogy compared to my past projects. I keep my pipeline 
 *              simple and straightforward. Heres how it works:
 *              
 *              The first line I initialize a timer. I'm sure you are curious on the simulation duration, so I have it 
 *              start at the very beginning. Next, I initialize a Medium object. This represents the space you want to
 *              do over-relaxation in. Next I add a disk held at constant potential, as well as several similar point
 *              charges. Next I use my SolveOR method, or Solve Over-Relaxation. This method takes four arguments,
 *              though you technically only need one: the Medium object in question. 
 *
 *              The other arguments are, in order, double alpha, which is the relaxation parameter, double tolerance, 
 *              which tells the simulation to stop once the error reaches below tolerance, and max_iters, which is
 *              usually set to 100_000 iterations. It takes about 3.5 minutes to run on my laptop given an alpha of 2.0.
 *              
 *              The main errors in this project arise from double precision errors, which remain very low considering 
 *              the values we have, and the error from the method of overrelaxation, but this program is designed for 
 *              you to set that tolerance as low as you want, though a tolerance of 0.00001 is generally fine for most 
 *              applications.
 *              
 */ 

using System.Diagnostics;

class Program
{
    static void Main(String[] args)
    {   
        // Time the render
        Stopwatch sw = new();
        sw.Start();

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
        Methods.SolveOR(medium, 2.0);

        // Stop Stopwatch
        sw.Stop();

        // Record the elapsed time
        TimeSpan elapsed = sw.Elapsed;

        // Write the duration of the sim to the console
        Console.WriteLine($"Time elapsed: {elapsed.TotalMilliseconds} ms");

        // Store the filepath in a string
        string filepath = Path.Combine(Environment.CurrentDirectory, "data.csv");

        // Output to a file
        Methods.ExportCSV(medium, filepath);
    }
}