using System;

class Program {
    static void Main(String[] args)
    {
        // Create a Projectile object with the desireable variables
        Projectile ball = new Projectile();

        // Euler's method (For quick debugging)
        Methods.EulersMethod(ball);

        // Write all the data to a .csv file.
        Methods.ToFile(ball.Positions, ball.Times);
    }
}