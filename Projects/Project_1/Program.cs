using System;
using System.Linq;

class Program {
    static void Main(String[] args)
    {
        // =============================================== Sim Pipeline ================================================
        // Create a Projectile object with the desirable variables
        Projectile ball = new Projectile();

        // Euler's method (For quick debugging)
        Methods.RK4(ball);

        // Write all the data to a .csv file.
        Methods.ToFile(ball);

        // ============================================== Flight Details ===============================================
        // Find the max height
        double maxHeight = ball.Positions.Max(p => p.Z);

        // Find the flight time
        double flightTime = ball.Times.Last();
        
        // Find where the projectile landed
        Vec3 landing_site = ball.Positions.Last();

        // Write the flight details to the console
        Console.WriteLine($"""
            Maximum Height: {maxHeight:F4} meters
            Flight Time: {flightTime:F4} seconds
            Landing Site: x: {landing_site[0]:F4},
                          y: {landing_site[1]:F4},
                          z: {landing_site[2]:F4}
            """);
    }
}