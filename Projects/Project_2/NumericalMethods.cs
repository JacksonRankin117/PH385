/*  Programmer: Jackson Rankin
 *        Date: January 19th, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This file holds the definition for the Methods class, which holds the behavior for my Euler-Cromer 
 *              integration method, as well as a file output for all the data. The errors arising from this file are 
 *              only from the Euler-Cromer method, which is susceptible to floating-point errors, though I use double-
 *              precision floats, and from the algorithm itself, which has an error on the order of dt, or in other 
 *              words, epsilon ~ 0.001 be default parameters. Of course, you can change this if you do a custom dt.
 *              
 */

class Methods
{
    public static void EulerCromer(Pendulum pend, double dt, double t_f)
    {
        while (pend._times.Last() < t_f)
        {
            // Find the new angular velocity
            pend._omegas.Add(pend._omegas.Last() + dt * (pend.GravityAcceleration(pend._thetas.Last())
                                                      +  pend.FrictionAcceleration(pend._omegas.Last())
                                                      +  pend.DrivingAcceleration(pend._times.Last())));
            
            // Find the new angular position
            double next_theta = pend._thetas.Last() + pend._omegas.Last() * dt;

            // Check if the current value for theta is outside [-pi, pi]
            if (next_theta < -Math.PI)
            {   
                // If theta < -pi, add 2pi
                next_theta += 2*Math.PI;
            } 
            else if (next_theta > Math.PI)
            {
                // If theta > pi, subtract 2pi
                next_theta -= 2*Math.PI;
            }

            pend._thetas.Add(next_theta);

            // Find the new time
            pend._times.Add(pend._times.Last() + dt);
        }
    }

    public static void ToFile(Pendulum pend, string filepath)
    {
        try
        {
            using StreamWriter sw = new StreamWriter(filepath);

            // Grab the telemetry data
            List<double> omega = pend._omegas;
            List<double> theta = pend._thetas;

            List<double> times = pend._times;

            // Write the header
            sw.WriteLine("t, theta, omega");

            for (int i = 0; i < theta.Count; i++)
            {
                // Store the current data through each iteration
                double p = theta[i];
                double v = omega[i];

                double t = times[i];

                // Write the data
                sw.WriteLine($"{t}, {p}, {v}");
            }

        }
        catch (Exception e)
        {   
            // If the program gets anything but the correct outputs, throw an error, and stop the program before it
            // crashes
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}