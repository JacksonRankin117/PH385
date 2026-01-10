using System;
using System.IO;

class ComputationalMethods
{
    // Absolutely insane ceiling for the number of loops
    public static int MAX_ITERS = 100_000_000;

    // ================================================ Euler's Method =================================================
    public static List<Vec3> EulersMethod(Projectile proj, double time_step)
    {   
        // Holds the time step argument as a double
        double dt = time_step;
        
        // Loop runs while the object remains above ground
        for (int i = 0; proj.Positions.Last()[2] >= 0.0; i++)
        {
            // Euler integration
            proj.Velocities.Add(proj.Velocities.Last() + proj.Accelerations.Last() * dt);
            proj.Positions.Add(proj.Positions.Last() + proj.Velocities.Last() * dt);

            // Calculate new acceleration. Accounts for gravity, drag, and the Magnus effect
            proj.Accelerations.Add(proj.g
                                 + proj.DragAcceleration(proj.Velocities.Last())
                                 + proj.MagnusAcceleration(proj.InitOmega, proj.Velocities.Last()));

            // Increment through time
            proj.Times.Add(proj.Times.Last() + dt);

            // Breaks the loop if the number of iterations gets too high
            if (i > MAX_ITERS)
            {
                break;
            }
        }
        return proj.Positions;
    }

    // ====================================================== RK4 ======================================================
    public static List<Vec3> RK4(Projectile proj, double time_step)
    {
        // Holds the time step argument as a double
        double dt = time_step;

        // Loop runs while the object remains above ground
        for (int i = 0; i < MAX_ITERS && proj.Positions.Last().Z > 0.0; i++)
        {
            // Fetch the current state at the start of each iteration
            Vec3 x = proj.Positions.Last();
            Vec3 v = proj.Velocities.Last();

            // Acceleration accounts for gravity, drag, and the Magnus effect
            Vec3 GetAccel(Vec3 currentVel) => proj.g + 
                                              proj.DragAcceleration(currentVel) + 
                                              proj.MagnusAcceleration(proj.InitOmega, currentVel);

            // RK4 Steps
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

            // Add to lists
            proj.Velocities.Add(v_new);
            proj.Positions.Add(x_new);
            proj.Accelerations.Add(GetAccel(v_new));
            proj.Times.Add(proj.Times.Last() + dt);
        }

        // Only return the positions for simplicity, though the other data is still accessible.
        return proj.Positions;
    }

    // ================================================== File Output ==================================================
    public static void ToFile(Projectile proj, string filepath)
    {

        try
        {
            using StreamWriter sw = new StreamWriter(filepath);

            // Grab the telemetry data
            List<Vec3> acc = proj.Accelerations;
            List<Vec3> vel = proj.Velocities;
            List<Vec3> pos = proj.Positions;

            List<double> times = proj.Times;

            // Write the header
            sw.WriteLine("t, x, y, z, vx, vy, vz, ax, ay, az");

            for (int i = 0; i < pos.Count; i++)
            {
                // Store the current data through each iteration
                Vec3 p = pos[i];
                Vec3 v = vel[i];
                Vec3 a = acc[i];

                double t = times[i];

                // Write the data
                sw.WriteLine($"{t}, {p[0]}, {p[1]}, {p[2]}, {v[0]}, {v[1]}, {v[2]}, {a[0]}, {a[1]}, {a[2]}");
            }

        }
        catch (Exception e)
        {   
            // If the program gets anything but the correct outputs, throw an error, and stop the program before it
            // crashes
            Console.WriteLine("Exception: " + e.Message);
        }
    }

    // ======================================= Console Output of Flight Details ========================================
    public static void FlightDetails(Projectile proj) {
        // Find the max height
        double maxHeight = proj.Positions.Max(p => p.Z);

        // Find the flight time
        double flightTime = proj.Times.Last();
        
        // Find where the projectile landed
        Vec3 landing_site = proj.Positions.Last();

        // Write the flight details to the console
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("Flight Details                                                By: Jackson Rankin");
        Console.WriteLine("================================================================================");
        Console.WriteLine($"""
                           Maximum Height: {maxHeight:F4} meters
                           Flight Time: {flightTime:F4} seconds
                           Landing Site: ({landing_site[0]:F4}, {landing_site[1]:F4}, {landing_site[2]:F4})
                           """);
    }
}