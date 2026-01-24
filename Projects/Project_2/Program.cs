/*  Programmer: Jackson Rankin
 *        Date: Jan 17th, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This is the main file for my Project 2 submission for PH 385, Numerical Modeling Physics. This 
 *              submission is much like my Project 1 submission, in terms of logic and structure. First, I sttaically
 *              call my UI.Initialize() method, whcih returns a tuple. This gathers all of the data I need to run the
 *              simulation. This includes all of the pendulum attributes, and the time step and final time for Euler-
 *              Cromer integration. When you run this file, you will be met with this in the terminal:
 *                  
 *
 *                  Pendulum Sim                                                  By: Jackson Rankin
 *                  ================================================================================
 *                  Would you like to use default settings? [Y/N]
 * 
 *
 *              This will allow you to either populate the pendulum with its default settings, or to manually populate 
 *              the pendulum with custum attributes. Hitting 'y' will call on the default constructor, which will
 *              populate all the attributes as if you entered them in manually like this:
 *              
 *              
 *                  Pendulum Initialization                                       By: Jackson Rankin
 *                  ================================================================================
 *                  Pendulum length [m]: 9.8
 *                  Damping Coefficient: 0.5
 *                  Initial angle [rad]: 0.2
 *                  Initial angular speed [rad/s]: 0
 *                  Initial time[s]: 0
 *                  Time step[s]: 0.001
 *                  Final time[s]: 180
 *                  Amplitude of driving force[N]: 1.2
 *                  Frequency of driving force[N]: 0.666666666666666667
 *              
 *              
 *              These are the default parameters, and will be entered in automatically. as soon as you hit enter, the 
 *              elapsed time will be printed to the console like this:
 *              
 *              
 *                  Elapsed time: 176.7305 ms
 *              
 *              
 *              If you hit 'n' on your keyboard, you will have to answer all of these attributes and conditions 
 *              manually, though it does allow for custom pendulum motion
 *              
 *       Error: The errors that arise from this simulation are from two sources: Euler-Cromer integration, and floating-
 *              point errors. I use double-precision floats for all of my data, so errors from this are minimal. If you 
 *              have any questions or concerns, feel free to email me. I feel as though I did will in documenting this 
 *              code, but I am more than willing to answer any questions that may arise
 *              
 */

using System.Diagnostics;

class Program
{
    static void Main(String[] args)
    {
        // =============================================== Sim Pipeline ================================================
        // Populate the important variables with desired values using a terminal UI
        (Pendulum pend, double dt, double t_f) = UI.Initialize();

        // Start the stopwatch
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Perform Euler-Cromer integration
        Methods.EulerCromer(pend, dt, t_f);

        // populate a string with the filepath destination
        string filepath = Path.Combine(Environment.CurrentDirectory, "data.csv");

        // Write the data to the filepath
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