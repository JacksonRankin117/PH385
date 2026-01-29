namespace Celestial
{
    class Program
    {
        static void Main(String[] args)
        {
            // =========================================== Celestial objects ===========================================
            Body Sun =    new Body(new Vec3(0, 0, 0),                    // Zero-vector. Sun at the origin
                                   new Vec3(0, 0, 0),                    // Zero-vector. Sun has zero velocity
                                   Constants.SolarMass);                 // Solar mass

            Body Earth =  new Body(new Vec3(1, 0, 0),                    // Position in AU
                                   new Vec3(0, 2 * Math.PI, 0),          // Velocity in AU/yr
                                   3.0027e-6 * Constants.SolarMass);     // Mass in Solar masses

            Body Jupiter = new Body(new Vec3(-5.2, 0, 0),                // Position in AU
                                    new Vec3(0, 0.876897 * Math.PI, 0),  // Velocity in AU/yr
                                    10*9.54588e-4 * Constants.SolarMass);   // Mass in Solar masses
            
            // Store each body in a list
            List<Body> bodies = [Sun, Earth, Jupiter];

            // Do the thang
            Methods.Verlet(bodies, 1e-4, 11.2);

            // Output to a file
            string filepath = Path.Combine(Environment.CurrentDirectory, "data.csv");
            Methods.ToFile(bodies, filepath, 1e-6);
        }
    }
}