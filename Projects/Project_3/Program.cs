/*  Programmer: Jackson Rankin
 *        Date: January 21st, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This is the main file for my Project 3 submission for PH 385, Numerical Modeling Physics. This 
 *              submission is similar to the last two projects in terms of the actual program, with a notable lack in
 *              UI. First, I initialize each body of orbit. Next, I populate the Sun's variables with a velocity which
 *              when summed with the two planet's velocities, makes the system's momentum equal to zero. Only floating 
 *              point errors arise from this, so its fairly accurate
 *              
 *              Next, I initialize a list of bodies which I then pass to a Verlet integrating method. It iterates 
 *              through each body in the list and calculates the next position according to Verlet. I also pass a time
 *              -step argument (years), as well as a final time argument, which dictates how long the sim runs (also in
 *              years). The errors from this occur only from Verlet and floating point errors, so its incredibly stable
 *              as we have a small dt and a stable algorithm.
 *              
 *              Next, we output each datapoint to a .csv file, and have a julia program read the .csv and make a 3D 
 *              animation (I prefer animations with Julia). In a separate file, a Python file will read the same .csv
 *              and will plot a static image of the entire celestial trajectory.
 *              
 */ 

namespace Celestial
{
    class Program
    {
        static void Main(String[] args)
        {
            // =========================================== Celestial objects ===========================================
            Body Earth =  new Body(new Vec3(1, 0, 0),                        // Position in AU
                                   new Vec3(0, 2 * Math.PI, 0),              // Velocity in AU/yr
                                   3.0027e-6 * Constants.SolarMass);         // Mass in Solar masses

            Body Jupiter = new Body(new Vec3(-5.2, 0, 0),                    // Position in AU
                                    new Vec3(0, -10.4*Math.PI/11.86, 0),     // Velocity in AU/yr
                                    1000*9.54588e-4 * Constants.SolarMass);  // Mass in Solar masses

            // Calculate new velocity of the sun to make the momentum of the system equal to zero
            Vec3 p_Earth = Earth._vel.Last() * Earth._mass;                  // Momentum of the Earth
            Vec3 p_Jupiter = Jupiter._vel.Last() * Jupiter._mass;            // Momentum of Jupiter

            Vec3 p_Sun = -(p_Earth + p_Jupiter);
            Vec3 v_Sun = p_Sun / Constants.SolarMass;

            Body Sun     = new(new Vec3(0, 0, 0),                  // Zero-vector. Sun at the origin
                                    v_Sun,                         // New velocity of the Sun
                                    Constants.SolarMass);          // Solar mass
            
            // Store each body in a list
            List<Body> bodies = [Sun, Earth, Jupiter];

            // Time Variables
            double dt = 1e-4;
            double t_f = 11.2;

            // Perform Verlet integration
            Methods.Verlet(bodies, dt, t_f);

            // Output to a file
            string filepath = Path.Combine(Environment.CurrentDirectory, "J=1000Jdata.csv");
            Methods.ToFile(bodies, filepath, dt);
        }
    }
}
