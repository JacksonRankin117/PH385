class Pendulum
{
    // Gravity
    public const double g = 9.8;  // Holds the values for gravitational acelleration

    // Pendulum Attributes
    public double _length;
    public double _damping_coeff;
    public double _init_theta;
    public double _init_omega;
    public double _driving_force;
    public double _driving_omega;
    public double _init_time;

    // Motion of the Pendulum 
    public List<double> _omegas; 
    public List<double> _thetas;
    public List<double> _times;
    
    // ================================================= Constructors ==================================================
    public Pendulum()
    {   
        // Default parameters
        _length        = 9.8;        // Length of the pendulum in meters
        _damping_coeff = 0.5;        // Damping coefficient
        _init_theta    = 0.2;        // Initial angle in radians in radians
        _init_omega    = 0.0;        // Initial angular velocity in rad/sec
        _init_time     = 0.0;        // Initial time in seconds
        _driving_force = 1.2;        // Driving force amplitude in Newtons
        _driving_omega = 2.0 / 3.0;  // Frequency of the driving force in rad/sec

        // Pendulum motion initialization
        _omegas = [_init_omega];     // Initializes the list of angular velocity with the initial angular velocity
        _thetas = [_init_theta];     // Initializes the list of angles with the initial angle
        _times  = [_init_time];      // Initializes the list of times with the initial time
    }
    // Pendulum(double length, double mass, double init_theta, double init_omega) {}

    // ==================================================== Methods ====================================================
    public double GravityAcceleration(double theta_i)
    {
        // Calculate and return the angular acceleration due to gravity
        return -g / _length * Math.Sin(theta_i);
    }
    public double FrictionAcceleration(double omega_i)
    {   
        // Calculate and return the angular acceleration due to friction
        return -_damping_coeff * omega_i;
    }
    public double DrivingAcceleration(double t_i)
    {   
        // Calculate and return the angular acceleration due to gravity
        return _driving_force * Math.Sin(_driving_omega * t_i);
    }

}