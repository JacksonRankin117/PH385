/*
 *  Programmer: Jackson Rankin
 *        Date: Dec 30th, 2025 (If you are Brother Kelley and are confused about this date, yes, this is a struct I made
 *                              before the semester started)
 *     Purpose: The purpose of this struct is to hold the information and behavior of a 3D Cartesian vector.
 * 
 *    Overview: This file holds a general purpose struct for a 3D vector using cartesian coordinates. This struct is 
 *              able to handle vector arithmetic, including the dot and cross products, scalar multiplication, and holds
 *              useful methods to find the distance between teo vectors, the projection of one vector on another, etc. 
 *              
 *  A few important things to note: 
 *   
 *    - Everything is in Cartesian coordinates, and this struct is only capable of arithmetic as of 1/7/2026
 *
 *    - This is a vector struct that I reuse for several projects, so if it looks like I put too much effort in for the 
 *      project at hand, that is because this struct is meant to apply to a general use.
*/

using System.Globalization;

public struct Vec3
{
    // Components
    public double X;
    public double Y;
    public double Z;

    // Tolerances to avoid floating-point errors
    public const double Epsilon = 1e-12;
    public const double Epsilon2 = 1e-6;

    // ================================================= Constructors ==================================================

    // Default constructor returns a zero-vector
    public Vec3() { }

    // You are able to initialize vector components with this constructor.
    public Vec3(double x, double y, double z) => (X, Y, Z) = (x, y, z);  

    // =============================================== Vector Properties ===============================================

    public readonly double MagnitudeSquared => (X * X) + (Y * Y) + (Z * Z);

    public readonly double Magnitude => Math.Sqrt(MagnitudeSquared);

    // Returns a new unit vector. Returns a zero vector if magnitude is below Epsilon.
    public readonly Vec3 Normalize()
    {
        double m = Magnitude;
        if (m < Epsilon) return new Vec3(0, 0, 0);
        return new Vec3(X / m, Y / m, Z / m);
    }

    // Indexer to access components via [0], [1], or [2].
    public double this[int i]
    {
        get => i switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new IndexOutOfRangeException("Vector index must be 0, 1, or 2.")
        };
    }


    // =============================================== Angle Conversions ===============================================
    
    public static double DegToRad(double deg) => deg * Math.PI / 180.0;  // Converts degrees to radians
    public static double RadToDeg(double rad) => rad * 180.0 / Math.PI;  // You'll just have to guess :)

    // =============================================== Vector Conversions ==============================================

    public static Vec3 FromCylindrical(double rho, double phi, double z)
    {
        // Return a new vector from ISO cylindrical coordinates
        return new Vec3(rho * Math.Cos(phi), rho * Math.Sin(phi), z);
    }

    public static Vec3 FromSpherical(double rho, double theta, double phi)
    {
        // Return a new vector from ISO spherical coordinates
        double sinTheta = Math.Sin(theta);  // Store the sine of theta in a double to save on performance (marginal)

        return new Vec3(
            rho * sinTheta * Math.Cos(phi),
            rho * sinTheta * Math.Sin(phi),
            rho * Math.Cos(theta)
        );
    }

    public readonly (double rho, double phi, double z) ToCylindrical()
    {
        return (Math.Sqrt(X * X + Y * Y), Math.Atan2(Y, X), Z);
    }

    public readonly (double rho, double theta, double phi) ToSpherical()
    {
        double rho2 = MagnitudeSquared;
        if (rho2 < Epsilon2) return (0.0, 0.0, 0.0);

        double rho = Math.Sqrt(rho2);
        // Clamp for safety with acos
        double cosTheta = Math.Clamp(Z / rho, -1.0, 1.0);
        return (rho, Math.Acos(cosTheta), Math.Atan2(Y, X));
    }

    // ================================================ Relational Math ================================================

    public static double AngleBetween(Vec3 a, Vec3 b)
    {
        // Check if either magnitudes are too small, and throw an error if they are.
        if (a.MagnitudeSquared < Epsilon2 || b.MagnitudeSquared < Epsilon2)
            throw new InvalidOperationException("Cannot calculate angle with a near-zero vector.");

        // If the magnitudes are both acceptable so floating point errors are no longer an issue, find the cosine of the
        // angle and clamp it on [-1, 1] for safety with acos.
        double cosAngle = Math.Clamp(Dot(a, b) / (a.Magnitude * b.Magnitude), -1.0, 1.0);

        // Return the angle in radians.
        return Math.Acos(cosAngle);
    }

    // Return the distance between two vectors squared.
    public static double DistanceSquared(Vec3 a, Vec3 b) => (a - b).MagnitudeSquared;

    // Return the distance between two vectors.
    public static double Distance(Vec3 a, Vec3 b) => (a - b).Magnitude;

    // =============================================== Operator Overloads ==============================================

    // Boolean Operators
    public static bool operator ==(Vec3 a, Vec3 b) =>
        Math.Abs(a.X - b.X) < Epsilon &&
        Math.Abs(a.Y - b.Y) < Epsilon &&
        Math.Abs(a.Z - b.Z) < Epsilon;

    public static bool operator !=(Vec3 a, Vec3 b) => !(a == b);

    // Vector Arithmetic
    public static Vec3 operator +(Vec3 a, Vec3 b) => new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vec3 operator -(Vec3 a, Vec3 b) => new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vec3 operator -(Vec3 v) => new Vec3(-v.X, -v.Y, -v.Z);
    public static Vec3 operator *(Vec3 v, double s) => new Vec3(v.X * s, v.Y * s, v.Z * s);
    public static Vec3 operator *(double s, Vec3 v) => v * s;
    
    public static Vec3 operator /(Vec3 v, double s)
    {
        if (Math.Abs(s) < Epsilon)
            return new Vec3(0, 0, 0);
        return v * (1.0 / s);
    }

    // Standard Dot and Cross product
    public static double Dot(Vec3 a, Vec3 b) => (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);

    public static Vec3 Cross(Vec3 a, Vec3 b) => new Vec3(
        a.Y * b.Z - a.Z * b.Y,
        a.Z * b.X - a.X * b.Z,
        a.X * b.Y - a.Y * b.X
    );

    // =================================================== Debugging ===================================================

    // Returns the vector as a tabulated string, with as much precision as you like.
    public string ToString(int decimals = 6, int columnWidth = 12)
    {
        string fmt = "F" + decimals;

        return string.Concat(
            X.ToString(fmt, CultureInfo.InvariantCulture).PadLeft(columnWidth),
            Y.ToString(fmt, CultureInfo.InvariantCulture).PadLeft(columnWidth),
            Z.ToString(fmt, CultureInfo.InvariantCulture).PadLeft(columnWidth)
        );
    }

    // Method that parses a string input to a Vec3
    public static bool TryParse(string s, out Vec3 v)
    {
        v = default;

        if (string.IsNullOrWhiteSpace(s))
            return false;

        s = s.Replace("(", "").Replace(")", "");

        string[] parts = s.Split(
            new[] { ' ', ',', '\t' },
            StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3)
            return false;

        if (!double.TryParse(parts[0], NumberStyles.Float,
            CultureInfo.InvariantCulture, out double x)) return false;

        if (!double.TryParse(parts[1], NumberStyles.Float,
            CultureInfo.InvariantCulture, out double y)) return false;

        if (!double.TryParse(parts[2], NumberStyles.Float,
            CultureInfo.InvariantCulture, out double z)) return false;

        v = new Vec3(x, y, z);
        return true;
    }

    // Grabs the hash code for the current Vec3 object
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Z);

    // Tests if two vectors are the same
    public override readonly bool Equals(object? obj) => obj is Vec3 other && this == other;
}