class Particle(Vec3 origin, double step_mag)
{   
    // Default coordinates for the origin
    public Vec3 _origin = origin;

    public Vec3 _pos = origin;

    // Random walk variables
    public static readonly Random rand = new();
    public readonly double _step = step_mag;

    // Return a random step, and increase the position
    public Vec3 RandStep()
    {
        // Find the correct distribution
        double u = 2 * rand.NextDouble() - 1;
        double phi = 2 * Math.PI * rand.NextDouble();

        // 
        double sinTheta = Math.Sqrt(1 - u * u);

        // Convert to cartesian coordinates
        double x = _step * sinTheta * Math.Cos(phi);
        double y = _step * sinTheta * Math.Sin(phi);
        double z = _step * u;

        return _pos += new Vec3(x, y, z);
    }

}