using System.Diagnostics;

class Program
{
    static void Main(String[] args)
    {
        // Start the stopwatch
        Stopwatch stopwatch = Stopwatch.StartNew();

        // =============================================== Sim Pipeline ================================================
        Pendulum pend = new Pendulum();
        Methods.EulerCromer(pend, 0.001, 180);
        string filepath = Path.Combine(Environment.CurrentDirectory, "data.csv");

        Methods.ToFile(pend, filepath);
        // =============================================== End Pipeline ================================================

        // Stop the stopwatch
        stopwatch.Stop();

        // Get the elapsed time as a TimeSpan value
        TimeSpan elapsed = stopwatch.Elapsed;

        // Display the elapsed time
        Console.WriteLine($"Elapsed time: {elapsed.TotalMilliseconds} ms");
    }
}