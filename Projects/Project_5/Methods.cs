using System.Text;

class Method
{
    // =================================================== Solve PDE ===================================================
    public static List<double[]> SolveWave(Cable cable, double r = 0.10, double epsilon = 0.001, double t_f = 0.25)
    {   
        double[] init_condition = cable.DefaultInitCondition();
        double L = cable.Length;    
        double M = cable.Segments;  

        double dx = L / M;         
        double dt = r * dx / 250;

        // We only want to save 500 frames for the whole 0.25s duration
        int total_frames_desired = 1500;
        int skip = (int)(t_f / dt / total_frames_desired);
        if (skip < 1) skip = 1; // Safety check

        List<double[]> u = [(double[])init_condition.Clone()];

        double t = 0.0;
        int iters = 0;
        int max_iters = 1_000_000; // Increased to accommodate small dt

        double c1 = 2 - 2*r*r - 6*epsilon*r*r*M*M;
        double c3 = r*r * (1 + 4*epsilon*M*M);
        double c4 = -epsilon * r*r * M*M;

        // We need these to track the state without saving every array to the List
        double[] u_prev = (double[])init_condition.Clone();
        double[] u_curr = (double[])init_condition.Clone();

        while (t <= t_f && iters < max_iters)
        {
            double[] u_next = new double[u_curr.Length];

            for (int i = 0; i < u_curr.Length; i++)
            {
                if (i <= 1 || i >= u_curr.Length - 2)
                {
                    u_next[i] = 0;
                }
                else
                {
                    double term1 = c1 * u_curr[i];
                    double term3 = c3 * (u_curr[i+1] + u_curr[i-1]);
                    double term4 = c4 * (u_curr[i+2] + u_curr[i-2]);

                    if (iters == 0)
                        u_next[i] = 0.5 * (term1 + term3 + term4);
                    else
                        u_next[i] = term1 - u_prev[i] + term3 + term4;
                }
            }

            // Only save to the List if it's a "skip" interval
            if (iters % skip == 0 && iters != 0)
            {
                u.Add((double[])u_next.Clone());
            }

            // Step the state forward: VERY IMPORTANT
            u_prev = u_curr;
            u_curr = u_next;

            t += dt;
            iters++;
        }
        
        return u;
    }

    // =================================================== CSV output ==================================================
    public static void ToFile(List<double[]> data, string filePath)
    {
        // Use a StringBuilder to minimize allocations while building the CSV rows
        StringBuilder csvContent = new StringBuilder();

        foreach (var row in data)
        {
            for (int i = 0; i < row.Length; i++)
            {
                csvContent.Append(row[i]);
                if (i < row.Length - 1)
                    csvContent.Append(",");
            }
            csvContent.AppendLine();
        }

        File.WriteAllText(filePath, csvContent.ToString());
        Console.WriteLine($"Data successfully written to: {filePath}");
    }
}