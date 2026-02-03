class Methods
{
    // Single SOR iteration
    public static void OverRelaxation(Medium3D medium, double alpha = 1.0)
    {
        int nx = medium.grid.GetLength(0);
        int ny = medium.grid.GetLength(1);
        int nz = medium.grid.GetLength(2);

        // Loop over interior points only
        for (int i = 1; i < nx - 1; i++)
        {
            for (int j = 1; j < ny - 1; j++)
            {
                for (int k = 1; k < nz - 1; k++)
                {
                    // Old potential
                    double V_old = medium.grid[i, j, k];

                    // Laplace's 7-point stencil average
                    double V_new = (medium.grid[i + 1, j, k] + medium.grid[i - 1, j, k]
                                  + medium.grid[i, j + 1, k] + medium.grid[i, j - 1, k]
                                  + medium.grid[i, j, k + 1] + medium.grid[i, j, k - 1]) / 6.0;

                    // Over-relaxation update
                    medium.grid[i, j, k] = V_old + alpha * (V_new - V_old);
                }
            }
        }
    }

    // Optional: iterate until convergence
    public static void SolveSOR(Medium3D medium, double alpha, double tol = 1e-6, int maxIter = 10000)
    {
        int iter = 0;
        double maxDiff;

        do
        {
            maxDiff = 0.0;
            int nx = medium.grid.GetLength(0);
            int ny = medium.grid.GetLength(1);
            int nz = medium.grid.GetLength(2);

            for (int i = 1; i < nx - 1; i++)
            {
                for (int j = 1; j < ny - 1; j++)
                {
                    for (int k = 1; k < nz - 1; k++)
                    {
                        double V_old = medium.grid[i, j, k];

                        double V_new = (medium.grid[i + 1, j, k] + medium.grid[i - 1, j, k]
                                      + medium.grid[i, j + 1, k] + medium.grid[i, j - 1, k]
                                      + medium.grid[i, j, k + 1] + medium.grid[i, j, k - 1]) / 6.0;

                        medium.grid[i, j, k] = V_old + alpha * (V_new - V_old);

                        double diff = Math.Abs(medium.grid[i, j, k] - V_old);
                        if (diff > maxDiff) maxDiff = diff;
                    }
                }
            }

            iter++;
        } while (maxDiff > tol && iter < maxIter);

        Console.WriteLine($"SOR finished in {iter} iterations, max change = {maxDiff}");
    }
}
