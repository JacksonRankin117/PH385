/*  Programmer: Jackson Rankin
 *        Date: January 21st, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This file holds the definition for the Methods class, which holds the behavior for my Verlet integration
 *              method, a gravition as well as a file output for all the data. The errors arising from this file are 
 *              only from the Verlet method, which is susceptible to floating-point errors, though I use double-
 *              precision floats, and from the algorithm itself, which has an error on the order of dt^2, or in other 
 *              words, epsilon ~ 0.000001 by default parameters. Of course, you can change this if you do a custom dt.
 *              
 */

public static class Methods
{
    public static void Verlet(List<Celestial.Body> bodies, double dt, double t_f)
    {
        double t = 0;

        // Initial acceleration for all bodies at t=0
        foreach (var body in bodies)
        {
            body._acc.Add(GravAccel(bodies, body) / body._mass);
        }

        while (t < t_f)
        {
            // 1. Update Positions: r(t + dt) = r(t) + v(t)dt + 0.5 * a(t)dt^2
            foreach (var body in bodies)
            {
                Vec3 nextPos = body._pos.Last() + body._vel.Last() * dt + 0.5 * body._acc.Last() * (dt * dt);
                body._pos.Add(nextPos);
            }

            // 2. Calculate new accelerations: a(t + dt) using the new positions
            // Note: We store these temporarily or add them to the list to use for velocity
            foreach (var body in bodies)
            {
                body._acc.Add(GravAccel(bodies, body) / body._mass);
            }

            // 3. Update Velocities: v(t + dt) = v(t) + 0.5 * (a(t) + a(t + dt))dt
            foreach (var body in bodies)
            {
                int lastIdx = body._acc.Count - 1;
                Vec3 nextVel = body._vel.Last() + 0.5 * (body._acc[lastIdx - 1] + body._acc[lastIdx]) * dt;
                body._vel.Add(nextVel);
            }

            t += dt;
        }
    }

    public static Vec3 GravAccel(List<Celestial.Body> bodies, Celestial.Body body_i)
    {
        Vec3 grav_force = new();

        foreach (var i in bodies) 
        {
            if (i == body_i)
            {
                continue;
            }

            grav_force += Celestial.Body.GravityVec(body_i, i);
        }

        return grav_force;
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
