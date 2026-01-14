using System;
using System.Numerics;

class Pendulum
{
    double _mass;
    double _length;
    double _init_theta;
    double _init_omega;
    double _damping_coeff;

    Pendulum()
    {   
        // Default parameters
        _mass          = 1;    // Mass of the pendulum in kgs
        _length        = 9.8;  // Length of the pendulum in meters
        _init_theta    = 0.2;  // Initial angle in radians
        _init_omega    = 0.0;  // Initial angular velocity
        _damping_coeff = 0.5;  // Damping coefficient
        



    }

    Pendulum(double length, double mass, double init_theta, double init_omega)
    {
        
    }
}