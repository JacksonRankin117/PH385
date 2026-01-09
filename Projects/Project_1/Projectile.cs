using System.Numerics;

class Projectile {

    // --------------------------------------------- Projectile Variables ----------------------------------------------

    // Constants
    public const double pi = Math.PI;
    public Vec3 g = new Vec3(0, 0, -9.81);

    // Projectile properties
    public double M;           // Holds the mass of the projectile in kgs
    public double A;           // Holds the cross-sectional area of the projectile
    public double C_D;         // Holds the Coefficient of Drag of the projectile

    // Environmental variables
    public double Rho;         // Holds the air density in kg/m^3

    // Projectile initial conditions
    public Vec3 InitPos;       // Holds the initial position from origin to the projectile in meters
    public Vec3 InitVel;       // Holds the initial velocity of the projectile
    public Vec3 InitOmega;     // Holds the initial spin vector of the projectile

    public double InitTime;    // Holds the initial value of time
    public double dt = 0.001;  // Holds the time step.

    // Holds the motion of the projectile
    public List<Vec3> Positions;
    public List<Vec3> Velocities;
    public List<Vec3> Accelerations;

    public List<double> time = new List<double>();

    // ------------------------------------------------- Constructors --------------------------------------------------
    // Default constructor will populate a new object with the desired variables, so I can concentrate on an adequate UI 
    // later
    public Projectile()
    {
        InitPos = new Vec3(0, 0, 10);
        InitVel = new Vec3(15, 5, 15);
        InitOmega = new Vec3(-20, 40, 20);
        InitTime = 0;
        
        M = 0.027;
        A = pi * 0.02 * 0.02;
        C_D = 0.50;
        Rho = 1.27;

        Positions = new List<Vec3> { InitPos };
        Velocities = new List<Vec3> { InitVel };
    }

    public Projectile(Vec3 pos, Vec3 vel, Vec3 omega, double time, double mass, double area, double c_d, double rho)
    {
        InitPos = pos;
        InitVel = vel;
        InitOmega = omega;
        InitTime = time;
        
        M = mass;
        A = area;
        C_D = c_d;
        Rho = rho;

        Positions = new List<Vec3> { pos };
        Velocities = new List<Vec3> { vel };
    }

    // ---------------------------------------------------- Methods ----------------------------------------------------

    Vec3 DragAcceleration(Vec3 vel)
    {
        double vmag = vel.Length();                           // Magnitude of the velocity
        double dragmag = 0.5 * Rho * C_D * A * vmag * vmag;   // Scalar drag force

        Vec3 drag = -dragmag * vel.Normalize();               // Drag vector opposite velocity
        return drag / M;                                      // Acceleration = force / mass
    }

    Vec3 MagnusAcceleration() {
        
    }


    
}