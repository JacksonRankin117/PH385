/*  Programmer: Jackson Rankin
 *        Date: January 7th, 2026
 *     Contact: ran23008@byui.edu
 * 
 *    Overview: This is the main file for my Project 1 submission for PH 385, Numerical Modelling Physics. In this file,
 *              I initialize a Projectile object called ball with my UI, which takes arguments from the user in the
 *              console, which looks like this once run:
 *              
 * 
 *                  Projectile Sim                                                By: Jackson Rankin
 *                  ================================================================================
 *                  Use default settings? [Y/N]
 * 
 * 
 *              By typing 'Y', "Default settings" will use the default constructor for projectile, and will automatically 
 *              populate the known variables as if the user selected 'N' on their key board, and manually entered values
 *              for each field like this:
 *              
 *
 *                  Projectile Initialization                                     By: Jackson Rankin
 *                  ================================================================================
 *                  Initial position (x y z) [m]: 0 0 5
 *                  Initial velocity (x y z) [m/s]: 4 4 10
 *                  Angular velocity (x y z) [rad/s]: -50 -100 100
 *                  Initial time [s]: 0
 *                  Mass [kg]: 0.0027
 *                  Cross-sectional area [m^2]: 0.00125663706144
 *                  Drag coefficient: 0.5
 *                  Air density [kg/m^3]: 1.27
 *                  Time Step[s]: 0.001
 *                  
 * 
 *
 *              This describes a ping pong ball with a radius of 2 cm, and therefore a cross-sectional area of 
 *              0.00125663706144 m^2. 
 *              
 *              Next, after I have given the Projectile object all the variables it needs to continue the simulation,
 *              I feed it into a computational method, in this case Runge-Kutta order 4, which will populate the
 *              Accelerations, Velocities, Positions, and Times lists of the Projectile object, all of which is directly
 *              made into a .csv file called data, so I can use a separate Python file called plot.py to visualize the 
 *              flight. The simulation automatically stops once the ball reaches a z-value of less than 0, i.e, hitting 
 *              the ground.
 * 
 *              After the simulation has run its course, the apex of the flight, the flight duration, and the landing
 *              coordinate is written to the console like this:
 *              
 *              
 *                  Flight Details                                                By: Jackson Rankin
 *                  ================================================================================
 *                  Maximum Height: 6.4926 meters
 *                  Flight Time: 2.3440 seconds
 *                  Landing Site: (3.6195, 4.7100, -0.0004)
 *              
 *
 *              The rest of my files won't be quite as documented, though I hope my comments on those files will be 
 *              documentation enough.
 */


using System;

class Program {
    static void Main(String[] args) {   
        // =============================================== Sim Pipeline ================================================
        // Create a Projectile object with the desirable variables
        (Projectile ball, double dt) = UI.Initialize();

        // RK4 method. Arguments include the Projectile object and a time step.
        ComputationalMethods.RK4(ball, dt);

        // ================================================ File Output ================================================
        // Write all flight telemetry data to a file
        string filepath = Path.Combine(Environment.CurrentDirectory, "Projects", "Project_1", "data.csv");
        
        ComputationalMethods.ToFile(ball, filepath);

        // ============================================== Flight Details ===============================================
        // Output the position of the apex, the flight duration, and landing coordinate.
        ComputationalMethods.FlightDetails(ball);
    }
}