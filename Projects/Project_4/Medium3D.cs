class Medium3D
{
    // Add an error
    readonly double epsilon = 1e-12;  // Max safe res for doubles

    // Physical constants
    readonly double k = 8.987e9;      // Coulomb's constant

    // Dimensions of the medium in meters
    public readonly double _x_lim_min;
    public readonly double _x_lim_max;
    public readonly double _y_lim_min;
    public readonly double _y_lim_max;
    public readonly double _z_lim_min;
    public readonly double _z_lim_max;

    // Points per meter
    public int _res;

    // Makes a grid with the dimensions and the resolution
    public double[,,] grid = new double[1, 1, 1];

    public Medium3D(double x_lim_min, double x_lim_max, 
                    double y_lim_min, double y_lim_max, 
                    double z_lim_min, double z_lim_max, 
                    int res)
    {   
        // Populate the dimensions of the medium
        _x_lim_min = x_lim_min;
        _x_lim_max = x_lim_max;
        _y_lim_min = y_lim_min;
        _y_lim_max = y_lim_max;
        _z_lim_min = z_lim_min;
        _z_lim_max = z_lim_max;

        // Finds the range of limits
        double del_x = x_lim_max - x_lim_min;
        double del_y = y_lim_max - y_lim_min;
        double del_z = z_lim_max - z_lim_min;

        // Number of points in each direction
        int x_res = res * Convert.ToInt32(del_x);
        int y_res = res * Convert.ToInt32(del_y);
        int z_res = res * Convert.ToInt32(del_z);

        // Finds the position step
        double dL = 1 / res;
    }
    
    // Returns the index of a grid point given the Cartesian coordinates of the point
    public (int x_index, int y_index, int z_index) GetIndex(double x_mag, double y_mag, double z_mag)
    {   
        int x_index = Convert.ToInt32(x_mag * _res);
        int y_index = Convert.ToInt32(y_mag * _res);
        int z_index = Convert.ToInt32(z_mag * _res);

        return (x_index, y_index, z_index);
    }

    // Return the Cartesian coordinates of a point given an index
    public (double x_coord, double y_coord, double z_mag) GetCoords(int x_index, int y_index, int z_index)
    {
        double x_coord = x_index / _res;
        double y_coord = y_index / _res;
        double z_coord = z_index / _res;

        return (x_coord, y_coord, z_coord);
    }

    // Adds a disk with fixed potential parallel to the XY plane
    public void AddDisk(double X, double Y, double Z, double radius, double V)
    {
        // Convert disk center to grid index
        var (cx, cy, cz) = GetIndex(X - _x_lim_min, Y - _y_lim_min, Z - _z_lim_min);

        // Convert radius to grid units
        int rIndex = (int)Math.Round(radius * _res);

        int xCount = grid.GetLength(0);
        int yCount = grid.GetLength(1);
        int zCount = grid.GetLength(2);

        // Only operate near the disk plane
        if (cz < 0 || cz >= zCount)
            return;

        for (int i = Math.Max(0, cx - rIndex); i <= Math.Min(xCount - 1, cx + rIndex); i++)
        {
            for (int j = Math.Max(0, cy - rIndex); j <= Math.Min(yCount - 1, cy + rIndex); j++)
            {
                // Distance from disk center in XY plane
                int dx = i - cx;
                int dy = j - cy;

                if (dx * dx + dy * dy <= rIndex * rIndex)
                {
                    grid[i, j, cz] = V;
                }
            }
        }
    }

    public void AddCharge(double X, double Y, double Z, double charge)
    {
        // Convert disk center to grid index
        var (cx, cy, cz) = GetIndex(X - _x_lim_min, Y - _y_lim_min, Z - _z_lim_min);

        int xCount = grid.GetLength(0);
        int yCount = grid.GetLength(1);
        int zCount = grid.GetLength(2);
        
    }
}