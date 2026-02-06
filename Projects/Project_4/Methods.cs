using System;

static class Methods
{
    // -------------------------------
    // Single SOR iteration
    // -------------------------------
    public static void OverRelaxation(Medium3D medium, double alpha = 1.0)
    {
        int nx = medium.grid.GetLength(0);
        int ny = medium.grid.GetLength(1);
        int nz = medium.grid.GetLength(2);

        double h2 = medium._dL * medium._dL;
        double eps = medium.epsilon0;

        for (int i = 1; i < nx - 1; i++)
        {
            for (int j = 1; j < ny - 1; j++)
            {
                for (int k = 1; k < nz - 1; k++)
                {
                    if (medium.isFixed[i, j, k])
                        continue;

                    double V_old = medium.grid[i, j, k];

                    double neighborAvg =
                        (medium.grid[i + 1, j, k] +
                         medium.grid[i - 1, j, k] +
                         medium.grid[i, j + 1, k] +
                         medium.grid[i, j - 1, k] +
                         medium.grid[i, j, k + 1] +
                         medium.grid[i, j, k - 1]) / 6.0;

                    double chargeTerm = medium.rho[i, j, k] * h2 / (6.0 * eps);

                    double V_new = neighborAvg + chargeTerm;

                    medium.grid[i, j, k] = V_old + alpha * (V_new - V_old);
                }
            }
        }
    }

    // -------------------------------
    // Solve until convergence
    // -------------------------------
    public static void SolveSOR(
        Medium3D medium,
        double alpha = 1.8,
        double tolerance = 1e-6,
        int maxIter = 10000)
    {
        int iter = 0;
        double maxDiff;

        int nx = medium.grid.GetLength(0);
        int ny = medium.grid.GetLength(1);
        int nz = medium.grid.GetLength(2);

        double h2 = medium._dL * medium._dL;
        double eps = medium.epsilon0;

        do
        {
            maxDiff = 0.0;

            for (int i = 1; i < nx - 1; i++)
            {
                for (int j = 1; j < ny - 1; j++)
                {
                    for (int k = 1; k < nz - 1; k++)
                    {
                        if (medium.isFixed[i, j, k])
                            continue;

                        double V_old = medium.grid[i, j, k];

                        double neighborAvg =
                            (medium.grid[i + 1, j, k] +
                             medium.grid[i - 1, j, k] +
                             medium.grid[i, j + 1, k] +
                             medium.grid[i, j - 1, k] +
                             medium.grid[i, j, k + 1] +
                             medium.grid[i, j, k - 1]) / 6.0;

                        double chargeTerm =
                            medium.rho[i, j, k] * h2 / (6.0 * eps);

                        double V_new = neighborAvg + chargeTerm;

                        medium.grid[i, j, k] =
                            V_old + alpha * (V_new - V_old);

                        double diff = Math.Abs(medium.grid[i, j, k] - V_old);
                        if (diff > maxDiff) maxDiff = diff;
                    }
                }
            }

            iter++;

        } while (maxDiff > tolerance && iter < maxIter);

        Console.WriteLine($"SOR finished in {iter} iterations | max change = {maxDiff:e}");
    }

    //
    public static void ExportCSV(Medium3D medium, string filename)
    {
        using StreamWriter writer = new StreamWriter(filename);

        int nx = medium.grid.GetLength(0);
        int ny = medium.grid.GetLength(1);
        int nz = medium.grid.GetLength(2);

        // Header
        writer.WriteLine("i,j,k,x,y,z,V");

        for (int i = 0; i < nx; i++)
        {
            for (int j = 0; j < ny; j++)
            {
                for (int k = 0; k < nz; k++)
                {
                    var (x, y, z) = medium.GetCoords(i, j, k);
                    double V = medium.grid[i, j, k];

                    writer.WriteLine($"{i},{j},{k},{x},{y},{z},{V}");
                }
            }
        }

        Console.WriteLine($"CSV exported to {filename}");
    }
}
