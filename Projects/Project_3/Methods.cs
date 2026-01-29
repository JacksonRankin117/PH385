using System.Collections.Generic;
using System.Linq;

public static class Methods
{
    public static void Verlet(List<Celestial.Body> bodies, double dt, double t_f)
    {
        double t = 0;

        // Initialize accelerations for the very first step
        UpdateAllAccelerations(bodies);

        //
        int iters = 0;

        while (t < t_f)
        {
            // 1. Half-step velocity and Full-step position
            foreach (var body in bodies)
            {
                // v(t + 0.5dt) = v(t) + 0.5 * a(t) * dt
                Vec3 halfVel = body._vel.Last() + (body._acc.Last() * 0.5 * dt);
                
                // r(t + dt) = r(t) + v(t + 0.5dt) * dt
                Vec3 nextPos = body._pos.Last() + (halfVel * dt);
                
                body._pos.Add(nextPos);
                // Temporarily store the half-velocity to finish the update later
                body._vel.Add(halfVel); 
            }

            // 2. Update accelerations based on NEW positions
            UpdateAllAccelerations(bodies);

            // 3. Finish the velocity update
            foreach (var body in bodies)
            {
                // v(t + dt) = v(t + 0.5dt) + 0.5 * a(t + dt) * dt
                Vec3 finalVel = body._vel.Last() + (body._acc.Last() * 0.5 * dt);
                
                // Replace the temporary half-velocity with the final velocity
                body._vel[body._vel.Count - 1] = finalVel;
            }
            iters++;

            if (iters > 20_000_000)
            {   
                Console.WriteLine(t);
                break;
            }
            t += dt;
        }
    }

    private static void UpdateAllAccelerations(List<Celestial.Body> bodies)
    {
        // Reset/Initialize current acceleration for all bodies to zero
        Dictionary<Celestial.Body, Vec3> currentAccels = new();
        foreach (var b in bodies) currentAccels[b] = new Vec3(0, 0, 0);

        // N-Body interaction loop
        for (int i = 0; i < bodies.Count; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                var b1 = bodies[i];
                var b2 = bodies[j];

                // F = (G * m1 * m2 * r_vec) / |r|^3
                // a = F / m
                Vec3 force = Celestial.Body.GravityVec(b1, b2);

                currentAccels[b1] += force / b1._mass;
                currentAccels[b2] -= force / b2._mass; // Newton's 3rd Law
            }
        }

        foreach (var b in bodies)
        {
            b._acc.Add(currentAccels[b]);
        }
    }

    public static void ToFile(List<Celestial.Body> bodies, string filepath, double dt)
    {
        try
        {
            using StreamWriter sw = new StreamWriter(filepath);

            // 1. Generate Dynamic Header: t, x1, y1, z1, x2, y2, z2...
            List<string> headers = new List<string> { "t" };
            for (int i = 0; i < bodies.Count; i++)
            {
                headers.Add($"x{i+1}");
                headers.Add($"y{i+1}");
                headers.Add($"z{i+1}");
            }
            sw.WriteLine(string.Join(", ", headers));

            // 2. Iterate through the recorded timesteps
            // We assume all bodies have the same number of recorded positions
            int rowCount = bodies[0]._pos.Count;

            for (int i = 0; i < rowCount; i++)
            {
                List<string> rowData = [];

                // Add time column
                double t = i * dt;
                rowData.Add(t.ToString("F8")); // Format to 4 decimal places

                // Add x, y, z for every body at this specific timestep
                foreach (var body in bodies)
                {
                    Vec3 p = body._pos[i];
                    rowData.Add(p.X.ToString("F6"));
                    rowData.Add(p.Y.ToString("F6"));
                    rowData.Add(p.Z.ToString("F6"));
                }

                sw.WriteLine(string.Join(", ", rowData));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}
