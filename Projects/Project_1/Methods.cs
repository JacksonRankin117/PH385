using System;
using System.IO;

class Methods
{

    public static List<Vec3> EulersMethod(Projectile proj)
    {   
        // Holds a variable for maximum iterations the loop can take
        double MAX_ITERS = 100_000;
        
        for (int i = 0; proj.Positions.Last()[2] >= 0.0; i++)
        {
            // Euler loop
            proj.Velocities.Add(proj.Velocities.Last() + proj.Accelerations.Last() * proj.dt);
            proj.Positions.Add(proj.Positions.Last() + proj.Velocities.Last() * proj.dt);

            proj.Accelerations.Add(proj.g
                                 + proj.DragAcceleration(proj.Velocities.Last()));
                                 //+ proj.MagnusAcceleration(proj.InitOmega, proj.Velocities.Last()));

            // Increment through time
            proj.Times.Add(proj.Times.Last() + proj.dt);

            // Breaks the loop if the number of iterations gets too high
            if (i > MAX_ITERS)
            {
                break;
            }
        }
        return proj.Positions;
    }

    public static List<Vec3> RK4(Projectile proj)
    {
        double MAX_ITERS = 100_000;
        for (int i = 0; proj.Positions.Last().Z >= 0.0 && i < MAX_ITERS; i++)
        {
            Vec3 x = proj.Positions.Last();
            Vec3 v = proj.Velocities.Last();
            double dt = proj.dt;

            // Correctly helper to calculate total acceleration
            Vec3 GetAccel(Vec3 currentVel) => 
                proj.g + proj.DragAcceleration(currentVel) + proj.MagnusAcceleration(proj.InitOmega, currentVel);

            // RK4 Sub-steps
            Vec3 k1v = GetAccel(v) * dt;
            Vec3 k1x = v * dt;

            Vec3 k2v = GetAccel(v + k1v * 0.5) * dt;
            Vec3 k2x = (v + k1v * 0.5) * dt;

            Vec3 k3v = GetAccel(v + k2v * 0.5) * dt;
            Vec3 k3x = (v + k2v * 0.5) * dt;

            Vec3 k4v = GetAccel(v + k3v) * dt;
            Vec3 k4x = (v + k3v) * dt;

            Vec3 v_new = v + (k1v + 2 * k2v + 2 * k3v + k4v) / 6.0;
            Vec3 x_new = x + (k1x + 2 * k2x + 2 * k3x + k4x) / 6.0;

            proj.Velocities.Add(v_new);
            proj.Positions.Add(x_new);
            proj.Accelerations.Add(GetAccel(v_new));
            proj.Times.Add(proj.Times.Last() + dt);
        }
        return proj.Positions;
    }


    public static void ToFile(List<Vec3> pos, List<double> times, string filepath = "/Users/jacksonrankin/Desktop/Student/7/PH385/Projects/Project_1/data.csv")
    {
        try
        {
            using StreamWriter sw = new StreamWriter(filepath);

            if (times.Count != pos.Count)
                throw new ArgumentException("times and pos must have the same length");


            // Write the header
            sw.WriteLine("t, x, y, z");

            for (int i = 0; i < pos.Count; i++)
            {
                Vec3 p = pos[i];
                sw.WriteLine($"{times[i]}, {p[0]}, {p[1]}, {p[2]}");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}