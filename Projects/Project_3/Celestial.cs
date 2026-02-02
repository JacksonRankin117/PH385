/*  Programmer: Jackson Rankin
 *        Date: January 21st, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This file holds the definition for a namespace called Celestial. I have a constants portion, which holds
 *              definitions for important constants like the name suggests. There is also a definition for a class 
 *              called Body, which holds all of the information that a massive body might need in three dimensions. This
 *              class also holds the methods that calculates the forces on and by a different body. Since no 
 *              computational methods are implemented here, the only errors that arise are from floating point 
 *              precision.
 *              
 */

namespace Celestial
{
    public static class Constants 
    {
        public const double SolarMass = 1.0;
        public static readonly double G = 4.0 * Math.PI * Math.PI;

    }
    public class Body
    {
        // Important paramaters for the body
        private Vec3 _init_pos;
        private Vec3 _init_vel;
        public double _mass;

        public List<Vec3> _pos;
        public List<Vec3> _vel;
        public List<Vec3> _acc;

        public Body(Vec3 pos, Vec3 vel, double mass)
        {
            _init_pos = pos;
            _init_vel = vel;
            _mass     = mass;

            _pos = [_init_pos];
            _vel = [_init_vel];
            _acc = [];

        }

        // Calculate the gravity between two objects
        public static Vec3 GravityVec(Body body1, Body body2)
        {
            double grav_const = Constants.G * body1._mass * body2._mass;

            Vec3 position = body2._pos.Last() - body1._pos.Last();

            return grav_const * position / Math.Pow(position.Magnitude, 3);
        }
    }
}