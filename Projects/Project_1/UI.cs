using System;
using System.Globalization;

class UI
{   
    // Initialize the UI
    public static (Projectile, double) Initialize()
    {   
        // Console UI header
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Projectile Sim                                                By: Jackson Rankin");
        Console.WriteLine("================================================================================");
        Console.WriteLine("Use default settings? [Y/N]");

        // Initialize char choice, to hold the user's choice
        char choice;

        // If the user doesn't put a 'Y' or an 'N', keep looping until a valid choice is selected.
        while (true)
        {
            choice = char.ToUpper(Console.ReadKey(true).KeyChar);
            Console.WriteLine();

            if (choice == 'Y' || choice == 'N')
                break;

            Console.WriteLine("Not a valid response. Please enter Y or N.");
        }

        // If the user selects the default settings, return a Projectile object made with the default constructor
        if (choice == 'Y')
            return (new Projectile(), 0.001);

        // If 'N' (the only plausible other letter the user is able to choose), populate the Projectile object with the 
        // user's preferences.
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Projectile Initialization                                     By: Jackson Rankin");
        Console.WriteLine("================================================================================");

        Vec3 pos   = ReadVec3("Initial position", " [m]");
        Vec3 vel   = ReadVec3("Initial velocity", " [m/s]");
        Vec3 omega = ReadVec3("Angular velocity", " [rad/s]");

        double time = ReadDouble("Initial time", " [s]");
        double mass = ReadDouble("Mass", " [kg]");
        double area = ReadDouble("Cross-sectional area", " [m^2]");
        double c_d  = ReadDouble("Drag coefficient");
        double rho  = ReadDouble("Air density", " [kg/m^3]");
        double dt   = ReadDouble("Time Step", "[s]");

        // Return the custom Projectile object.
        return (new Projectile(
            pos,
            vel,
            omega,
            time,
            mass,
            area,
            c_d,
            rho
        ), dt);
    }


    static double ReadDouble(string prompt, string units = "")
    {
        while (true)
        {
            Console.Write($"{prompt}{units}: ");
            string? input = Console.ReadLine();

            if (double.TryParse(input,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double value))
                return value;

            Console.WriteLine("Invalid number.");
        }
    }

    static Vec3 ReadVec3(string label, string units = "")
    {
        while (true)
        {
            Console.Write($"{label} (x y z){units}: ");
            string? input = Console.ReadLine();

            if (Vec3.TryParse(input!, out Vec3 v))
                return v;

            Console.WriteLine("Invalid vector. Example: 1 0 -3");
        }
    }
}
