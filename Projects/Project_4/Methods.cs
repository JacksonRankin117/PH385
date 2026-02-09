/*  Programmer: Jackson Rankin
 *        Date: February 3rd, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This is the main file for my Project 4 submission for PH 385, Numerical Modeling Physics. This 
 *              submission presents no change in style or idealogy compared to my past projects. I keep my pipeline 
 *              simple and straightforward, minimizing abstraction and prioritizing clarity of the numerical method.
 *              These methods implement Successive Over Relaxation (SOR) to solve Poisson’s equation in 3D space.
 *              Additional helper utilities are included for convergence solving and exporting data for visualization.
 *              
 *              Errors will present in claculations involving doubles, as well as the numerical method used. But the
 *              numerical method in question is such that you are able to reduce the error to a tolerance of our
 *              choosing.
 *              
 */ 

static class Methods
{
    // Single over relaxation iteration
    public static void OverRelaxation(Medium3D medium, double alpha = 1.0)
    {   
        // Number of components in the x, y, z
        int nx = medium.grid.GetLength(0);
        int ny = medium.grid.GetLength(1);
        int nz = medium.grid.GetLength(2);

        // Square of the spatial step size (used in finite difference approximation)
        double h2 = medium._dL * medium._dL;

        // Permittivity constant for the medium
        double eps = medium.epsilon0;

        // Iterte through each index
        for (int i = 1; i < nx - 1; i++)
        {
            for (int j = 1; j < ny - 1; j++)
            {
                for (int k = 1; k < nz - 1; k++)
                {   
                    // If the particular tile is held at a fixed value, continue
                    if (medium.isFixed[i, j, k])
                        continue;

                    // grabs the old value
                    double V_old = medium.grid[i, j, k];

                    // Calculates the average of all the neighboring gridvals
                    // This is the discrete Laplacian portion of Poisson's equation
                    double neighborAvg =
                        (medium.grid[i + 1, j, k] +
                         medium.grid[i - 1, j, k] +
                         medium.grid[i, j + 1, k] +
                         medium.grid[i, j - 1, k] +
                         medium.grid[i, j, k + 1] +
                         medium.grid[i, j, k - 1]) / 6.0;

                    // Charge density contribution from Poisson equation
                    // ∇²V = -ρ/ε → rearranged into finite difference form
                    double chargeTerm = medium.rho[i, j, k] * h2 / (6.0 * eps);

                    // Computes the relaxed update target
                    double V_new = neighborAvg + chargeTerm;

                    // Applies over relaxation update rule
                    medium.grid[i, j, k] = V_old + alpha * (V_new - V_old);
                }
            }
        }
    }

    // ******************************************** Solve until convergence ********************************************
    public static void SolveOR(
        Medium3D medium,
        double alpha = 1.8,
        double tolerance = 1e-6,
        int maxIter = 100_000)
    {
        // Keeps track of the iterations and the maximum difference
        int iter = 0;
        double maxDiff;

        // Number of elements in the x, y, z
        int nx = medium.grid.GetLength(0);
        int ny = medium.grid.GetLength(1);
        int nz = medium.grid.GetLength(2);

        // Square of grid spacing for finite difference scaling
        double h2 = medium._dL * medium._dL;

        // Permittivity constant used in charge term
        double eps = medium.epsilon0;

        // Same code as before, but iterate until convergence.
        do
        {   
            maxDiff = 0.0;

            for (int i = 1; i < nx - 1; i++)
            {
                for (int j = 1; j < ny - 1; j++)
                {
                    for (int k = 1; k < nz - 1; k++)
                    {
                        // Skip fixed boundary or constraint nodes
                        if (medium.isFixed[i, j, k])
                            continue;

                        // Store current potential value
                        double V_old = medium.grid[i, j, k];

                        // Neighbor averaging (discrete Laplacian)
                        double neighborAvg =
                            (medium.grid[i + 1, j, k] +
                             medium.grid[i - 1, j, k] +
                             medium.grid[i, j + 1, k] +
                             medium.grid[i, j - 1, k] +
                             medium.grid[i, j, k + 1] +
                             medium.grid[i, j, k - 1]) / 6.0;

                        // Charge density correction term
                        double chargeTerm =
                            medium.rho[i, j, k] * h2 / (6.0 * eps);

                        // New relaxed potential target
                        double V_new = neighborAvg + chargeTerm;

                        // Apply SOR update
                        medium.grid[i, j, k] =
                            V_old + alpha * (V_new - V_old);

                        // Track the largest absolute change for convergence test
                        double diff = Math.Abs(medium.grid[i, j, k] - V_old);
                        if (diff > maxDiff) maxDiff = diff;
                    }
                }
            }

            // Increment iteration counter
            iter++;

        } while (maxDiff > tolerance && iter < maxIter);

        // Report convergence statistics
        Console.WriteLine($"SOR finished in {iter} iterations | max change = {maxDiff:e}");
    }

    // Get this to a csv file
    public static void ExportCSV(Medium3D medium, string filename)
    {
        using StreamWriter writer = new(filename);

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
                    // Get the indeces and values at each coordinate
                    var (x, y, z) = medium.GetCoords(i, j, k);
                    double V = medium.grid[i, j, k];

                    // Write it out.
                    writer.WriteLine($"{i},{j},{k},{x},{y},{z},{V}");
                }
            }
        }

        // Tell the user the method completed, and we now have a data file.
        Console.WriteLine($"CSV exported to {filename}");
    }
}
