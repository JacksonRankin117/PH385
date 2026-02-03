
class Program
{
    static void Main(String[] args)
    {
        // Crete a new medium
        Medium3D medium = new(-0.5, 0.5, -0.5, 0.5, -0.5, 0.5, 100);

        // Add a disk held at constant potential
        medium.AddDisk(0, 0, 0, 0.1, 0.25);

        // Add point charges
        medium.AddCharge(0.250, 0.000, 0.00,  1e-6);
        medium.AddCharge(0.000, 0.250, 0.00,  1e-6);
        medium.AddCharge(-0.25, 0.000, 0.00, -1e-6);
        medium.AddCharge(0.000, -0.25, 0.00, -1e-6);

        // Run the numerical method
        Methods.OverRelaxation(medium, 1.0);

    }
}