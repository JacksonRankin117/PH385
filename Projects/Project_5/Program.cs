/*  Programmer: Jackson Rankin
 *        Date: February 9th, 2026
 *     Contact: ran23008@byui.edu
 * 
 *    Overview: This is the main file for my Project 5 submission for PH 385, Numerical Modelling Physics. In this file,
 *              I solve the wave equation using a few classes and methods.
 *              
 *              First I initialize a Cable class object. I named it 'Cable' because 'String' was already taken, probably
 *              by some unimportant datatype.
 *              
 *              While I wanted to make it so the user could define any initial condition without digging through the
 *              code, I wasn't able to implement a clean approach in time. Definitely something to investigate in the 
 *              future.
 *              
 *              Next, I pass the Cable object as an argument for another function, which solves the wave equation. This
 *              function has a bunch of default parameters, namely the timestep ratio r, the stiffness coefficient
 *              epsilon, and the final time t_f, which is the temporal limit of the solution.
 *              
 *              I store this solution as a double array called 'u'. I find the path to the directory and pass it off in
 *              the filesave method. The Method.ToFile takes 'u' and the filepath and generates a .csv file, where it 
 *              can then be interpreted by in this case, plot.jl
 *              
 *              A typical output should look like this in the terminal:
 *
 * ---------------------------------------------------------------------------------------------------------------------
 *          
 *          PS C:\Users\jrank\Desktop\School\Semester 7\PH385\Projects\Project_5> dotnet run
 *          Solving wave equation...
 *          Number of iterations: 625000
 *          Data successfully streamed to: C:\Users\jrank\Desktop\School\Semester 7\PH385\Projects\Project_5\data.csv
 *          Wrote file to C:\Users\jrank\Desktop\School\Semester 7\PH385\Projects\Project_5\data.csv
 *              
 * ---------------------------------------------------------------------------------------------------------------------
 *              
 *              The limitations to this program are from the specific method used, which is essentially Jacobi 
 *              relaxation, and from any double precision floating point errors. This is a fast and accurate solver.
 *
 */

class Program
{
    static void Main(string[] args)
    {
        // Initialize the cable object with the default constructor. Also solves the initial condition
        Cable cable = new();

        // Solve the wave equation
        Console.WriteLine("Solving wave equation...");
        List<double[]> u = Method.SolveWave(cable);

        // Define path (Using .csv extension for easy opening in Excel/Python)
        string filepath = Path.Combine(Environment.CurrentDirectory, "data.csv");

        // Export
        Method.ToFile(u, filepath);

        // Tell the user the program is done running, and a file has been created.
        Console.WriteLine("Wrote file to " + filepath);
    }
}