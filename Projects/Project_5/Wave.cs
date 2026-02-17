/*  Programmer: Jackson Rankin
 *        Date: February 9th, 2026
 *     Contact: ran23008@byui.edu
 * 
 *    Overview: This file houses the class definition for my Cable class. This class initializes the condition of the
 *              wave on the string, or 'cable' at t = 0s. The length, speed, and number of segments may be altered,
 *              though you may have a tough time in data collection. It may be wise in the future to modify the Method
 *              class to only write some datapoints to the file, which would increase the data processing speed.
 *              
 *              One other important thing this file does is clamp the endpoints at y = 0
 *              
 *              The only errors originate from doubles and from the modified Jacobi relaxation loop.
 *              
 *              
 */


class Cable(double length = 1.0, double speed = 250, int segments = 100)
{   
    // Populate necessary variables.
    public double Length { get; } = length;
    public double Speed { get; } = speed;
    public int Segments { get; } = segments;
    public double Dx => Length / Segments;

    // Method to populate the initial condition
    public double[] DefaultInitCondition()
    {
        // If Segments is 100, we need 101 points to include both 0.0 and 1.0
        double[] u = new double[Segments + 1];

        for (int i = 0; i < u.Length; i++) // Standard safe way to loop through arrays
        {
            double x = i * Dx;

            // Boundary condition: If it's the first or last index, force it to 0
            if (i == 0 || i == Segments)
            {
                u[i] = 0;
            }
            else
            {
                u[i] = GaussWave( 0.01, 0.50, 0.10, x)
                     + GaussWave( 0.01, 0.35, 0.05, x)
                     + GaussWave(-0.01, 0.75, 0.03, x);
            }
        }

        return u;
    }

    // define a Gaussian wave. Essentially a bell curve
    public static double GaussWave(double A, double x0, double w, double i)
    {
        return A * Math.Exp(-Math.Pow((i - x0), 2) / (2*w));
    }
}
