/*  Programmer: Jackson Rankin
 *        Date: January 19th, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This file holds the definition for the UI class. The purpose of this class is to gather data from the 
 *              user in the terminal. The Initialize() method will do exactly that. I also have a custom ReadDouble()
 *              method that will loop each prompt over and over again until the user inputs a string that can be parsed
 *              into a double.
 *              
 */

using System.Globalization;

class UI
{   
    // Returns a new pendulum object, double dt, and 
    public static (Pendulum, double, double) Initialize()
    {   
        // Clear the terminal for a cleaner setup, and display the main menu
        Console.Clear();
        Console.WriteLine("Pendulum Sim                                                  By: Jackson Rankin");
        Console.WriteLine("================================================================================");
        Console.WriteLine("Would you like to use default settings? [Y/N]");

        // This char holds the user's choice
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
            return (new Pendulum(), 0.001, 180);
        
        // If 'N' (the only plausible other letter the user is able to choose), populate the Projectile object with the 
        // user's preferences.
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Pendulum Initialization                                       By: Jackson Rankin");
        Console.WriteLine("================================================================================");

        // Pendulum attributes
        double length          = ReadDouble("Pendulum length", "[m]");
        double damping_coeff   = ReadDouble("Damping Coefficient");
        double initial_theta   = ReadDouble("Initial angle ", "[rad]");
        double initial_omega   = ReadDouble("Initial angular speed ", "[rad/s]");

        // Time attributes
        double initial_time    = ReadDouble("Initial time ", "[s]");
        double delta_time      = ReadDouble("Time step ", "[s]");
        double final_time      = ReadDouble("Final time ", "[s]");

        // Driving force attributes
        double driving_force   = ReadDouble("Amplitude of driving force ", "[N]");
        double driving_freq    = ReadDouble("Frequency of driving force ", "[N]");

        // Write a new line to better separate the input from the stopwatch duration output
        Console.WriteLine("");

        // Return the custom Projectile object.
        return (new Pendulum(length, damping_coeff,                       // Pendulum attributes
                             initial_theta, initial_omega, initial_time,  // Initial conditions
                             driving_force, driving_freq),                // Driving force attributes
                             delta_time, final_time);                     // Time Attributes
    }

    // Custom ReadDouble function takes a prompt and a unit argument
    static double ReadDouble(string prompt, string units = "")
    {   
        // This will loop until the user inputs a string that can be parsed to a double value.
        while (true)
        {   
            // Write out the prompt and units
            Console.Write($"{prompt}{units}: ");

            // Hope its a string
            string? input = Console.ReadLine();

            // Check if it can be parsed to a double.
            if (double.TryParse(input,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double value))

                // If it can, return the double
                return value;

            // If it can't, tell the user they are being dumb and display the prompt with units again
            Console.WriteLine("Invalid number.");
        }
    }
}