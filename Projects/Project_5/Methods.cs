/*  Programmer: Jackson Rankin
 *        Date: February 9th, 2026
 *     Contact: ran23008@byui.edu
 * 
 *    Overview: This file houses the class definition for my Method class. This class solves the wave equation, and
 *              outputs the data to a csv. The data file is quite large, around 1.23 GB in default conditions, so the 
 *              program may take a few moments to run, depending on the system.
 *              
 *              As said in Program.cs, the only errors originate from doubles and from the modified Jacobi relaxation 
 *              loop.
 *              
 *              
 */

using System.Text;

class Method
{
    // =================================================== Solve PDE ===================================================
    public static List<double[]> SolveWave(Cable cable, double r = 0.01, double epsilon = 0.001, double t_f = 0.25)
    {   
        // Populate the necessary variables.
        double[] init_condition = cable.DefaultInitCondition();
        double L = cable.Length;    
        double M = cable.Segments;  

        double dx = L / M;         
        double dt = r * dx / 250;

        List<double[]> u = [(double[])init_condition.Clone()];

        double t = 0.0;
        int iters = 0;
        int max_iters = 1_000_000; // The nominal iteration count should be 625,000

        // Calculate various coefficients, saving a bit of time
        double c1 = 2 - 2*r*r - 6*epsilon*r*r*M*M;
        double c3 = r*r * (1 + 4*epsilon*M*M);
        double c4 = -epsilon * r*r * M*M;

        // We need these to track the state without saving every array to the List
        double[] u_prev = (double[])init_condition.Clone();
        double[] u_curr = (double[])init_condition.Clone();

        // Run the loop
        while (t <= t_f)
        {   
            // Initialize the double array to hold the iterated solution
            double[] u_next = new double[u_curr.Length];

            // Iterate through each segment
            for (int i = 0; i < u_curr.Length; i++)
            {
                if (i <= 1 || i >= u_curr.Length - 2)
                {   
                    // Set the endpoints to 0
                    u_next[i] = 0;
                }
                else
                {   
                    // Find the iterated solution at the index
                    double term1 = c1 *  u_curr[i];
                    double term3 = c3 * (u_curr[i+1] + u_curr[i-1]);
                    double term4 = c4 * (u_curr[i+2] + u_curr[i-2]);

                    // If this is the first iteration, find the next solution
                    if (iters == 0)
                        u_next[i] = 0.5 * (term1 + term3 + term4);
                    else
                        u_next[i] = term1 - u_prev[i] + term3 + term4;
                }
            }

            // Inform the user the loop repeated more than the maximum number of iterations
            if (iters > max_iters) {
                Console.WriteLine("Maximum iterations reached.");
                break;
            }

            // Only save the first 20000 data points
            //if (iters < 50_000) 
            //{
            // Or 
            u.Add((double[])u_next.Clone());

            //}

            // Step the state forward
            u_prev = u_curr;
            u_curr = u_next;

            // increase the time step and iteration count.
            t += dt;
            iters++;
        }
        Console.WriteLine("Number of iterations: " + iters);
        return u;
    }

    // =================================================== CSV output ==================================================
    public static void ToFile(List<double[]> data, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var row in data)
            {
                // Join the double array into a comma-separated string and write immediately
                writer.WriteLine(string.Join(",", row));
            }
        }
        Console.WriteLine($"Data successfully streamed to: {filePath}");
    }
}