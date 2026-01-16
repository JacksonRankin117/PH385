/*  Programmer: Jackson Rankin
 *        Date: January 7th, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This file holds the definition for a class called Projectile, which holds all of the information that 
 *              a projectile may need. This class also holds the methods that calculates the external forces on the 
 *              projectile excluding gravity, which is held as a Vec3 object called g. Since no computational methods
 *              are implemented here, the only errors that arise are from floating point precision.
 *              
 *              
 *              
 */

class Projectile {

    // ============================================= Projectile Variables ==============================================

    // Constants
    public Vec3 g = new (0, 0, -9.81);

    // Projectile properties
    public double M;                  // Holds the mass of the projectile in kgs
    public double A;                  // Holds the cross-sectional area of the projectile
    public double C_D;                // Holds the Coefficient of Drag of the projectile

    // Environmental variables
    public double Rho;                // Holds the air density in kg/m^3

    // Projectile initial conditions
    public Vec3 InitPos;              // Holds the initial position from origin to the projectile in meters
    public Vec3 InitVel;              // Holds the initial velocity of the projectile
    public Vec3 InitOmega;            // Holds the initial spin vector of the projectile
    public Vec3 InitAcceleration;     // Holds the value for the initial acceleration of the projectile

    public double InitTime;           // Holds the initial value of time

    // Holds the motion of the projectile
    public List<Vec3> Positions;      // Will hold all of the positions at every given time
    public List<Vec3> Velocities;     // Will hold all of the velocities at every given time
    public List<Vec3> Accelerations;  // Will hold all of the accelerations at every given time
    public List<double> Times;        // Will hold all of the steps in time

    // ================================================= Constructors ==================================================
    // Default constructor will populate a new object with the desired variables, so I can concentrate on an adequate UI 
    // later
    public Projectile()
    {
        // Set the desired constants
        M = 0.0027;
        A = Math.PI * 0.02 * 0.02;
        C_D = 0.5;
        Rho = 1.27;

        // Set the initial conditions
        InitPos = new Vec3(0, 0, 5);
        InitVel = new Vec3(4, 4, 10);
        InitOmega = new Vec3(-50, -100, 100);
        InitTime = 0;

        // Calculate initial acceleration
        InitAcceleration = g + DragAcceleration(InitVel) + MagnusAcceleration(InitOmega, InitVel);

        Positions = [InitPos];
        Velocities = [InitVel];
        Accelerations = [InitAcceleration];
        Times = [InitTime];
    }

    // This constructor takes various arguments to cater to the user.
    public Projectile(Vec3 pos, Vec3 vel, Vec3 omega, double time, double mass, double area, double c_d, double rho)
    {
        // Populates the initial condition with the user's preference
        InitPos = pos;
        InitVel = vel;
        InitOmega = omega;
        InitTime = time;

        // Calculate initial acceleration
        InitAcceleration = DragAcceleration(InitVel) + MagnusAcceleration(InitOmega, InitVel) + g;
        
        // Populates the projectile and environment variables
        M = mass;
        A = area;
        C_D = c_d;
        Rho = rho;

        // Populate the list with initial conditions
        Positions = [InitPos];
        Velocities = [InitVel];
        Accelerations = [InitAcceleration];
        Times = [InitTime];
    }

    // ======================================= Acceleration Calculation Methods ========================================

    // Approximates the acceleration due to air resistance.
    public Vec3 DragAcceleration(Vec3 vel)
    {
        // Calculate the force due to drag and store it as a vector
        Vec3 F_D = -0.5 * Rho * A * C_D * vel.Magnitude * vel;

        // Return the acceleration by dividing by mass.
        return F_D / M; 
    }

    // Approximates the acceleration due to the Magnus effect
    public Vec3 MagnusAcceleration(Vec3 omega, Vec3 vel, double S0divM = 0.040) {
        // Calculates the Magnus acceleration of a spinning projectile, assuming a spinning ball
        return S0divM * Vec3.Cross(omega, vel);
    }
}