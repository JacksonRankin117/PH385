class Cable(double length = 1.0, double speed = 250, int segments = 100)
{
    public double Length { get; } = length;
    public double Speed { get; } = speed;
    public int Segments { get; } = segments;
    public double Dx => Length / Segments;

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

    public static double GaussWave(double A, double x0, double w, double i)
    {
        return A * Math.Exp(-Math.Pow((i - x0), 2) / (2*w));
    }
}
