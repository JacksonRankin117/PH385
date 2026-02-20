class Cube(double low_x, double high_x,
           double low_y, double high_y,
           double low_z, double high_z)
{
    // Bounds
    readonly double _low_x = low_x;
    readonly double _low_y = low_y;
    readonly double _low_z = low_z;

    readonly double _high_x = high_x;
    readonly double _high_y = high_y;
    readonly double _high_z = high_z;

    // Populate the center of the cube with a bunch of particles
    public List<Particle> Populate(int num_particle, double step_size)
    {   
        List<Particle> particles = [];

        for (int i = 1; i < num_particle; i++)
        {
            particles.Add(new Particle(new(), step_size));
        }

        return particles;
    }
    
    // Check if the particle is out of bounds
    public void Reflect(Particle p)
    {
        Vec3 pos = p._pos;

        // X axis
        if (pos.X > _high_x)
            pos.X = _high_x - (pos.X - _high_x);
        else if (pos.X < _low_x)
            pos.X = _low_x + (_low_x - pos.X);

        // Y axis
        if (pos.Y > _high_y)
            pos.Y = _high_y - (pos.Y - _high_y);
        else if (pos.Y < _low_y)
            pos.Y = _low_y + (_low_y - pos.Y);

        // Z axis
        if (pos.Z > _high_z)
            pos.Z = _high_z - (pos.Z - _high_z);
        else if (pos.Z < _low_z)
            pos.Z = _low_z + (_low_z - pos.Z);

        p._pos = pos;
    }

    // Initiate diffusion. Output position and rms displacement
    public void Diffuse(List<Particle> particles,
                    string positionsPath,
                    string spreadPath,
                    int steps = 1000)
    {
        using StreamWriter pos = new(positionsPath);
        using StreamWriter spr = new(spreadPath);

        for (int i = 0; i < steps; i++)
        {
            double r2sum = 0;

            foreach (var p in particles)
            {
                p.RandStep();   // move
                Reflect(p);     // bounce

                double r2 =
                    p._pos.X * p._pos.X +
                    p._pos.Y * p._pos.Y +
                    p._pos.Z * p._pos.Z;

                r2sum += r2;

                pos.WriteLine($"{i},{p._pos.X},{p._pos.Y},{p._pos.Z}");
            }

            double mean_r2 = r2sum / particles.Count;

            spr.WriteLine($"{i},{mean_r2}");
        }
    }
}