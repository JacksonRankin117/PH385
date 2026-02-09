/*  Programmer: Jackson Rankin
 *        Date: February 1st, 2026
 *     Contact: ran23008@byui.edu
 *
 *    Overview: This file houses the class definition for Medium3D. This class defines a 3D space that can be used for
 *              the method of over-relaxation, and can store accurate electric potential values in 3D space.
 *              
 *              The methods in this class allow you to add point charges, fixed potential disks, and for every x, y, z
 *              coordinate you get an i, j, k, index, and vice versa.
 *              
 *              There are no numerical methods, but I do have some simple calculations made with doubles, and as such,
 *              there will be errors inherent to doubles in those calculations.
 *              
 */ 

class Medium3D
{
    // Physical constants
    public readonly double epsilon0 = 8.854187817e-12;

    // Domain limits
    public readonly double _x_lim_min;
    public readonly double _x_lim_max;
    public readonly double _y_lim_min;
    public readonly double _y_lim_max;
    public readonly double _z_lim_min;
    public readonly double _z_lim_max;

    // Spatial step
    public readonly double _dL;
    public readonly int _res;

    // Storage grids
    public double[,,] grid;
    public bool[,,] isFixed;
    public double[,,] rho;

    // Only constructor. You cannot initialize a medium with default attributes.
    public Medium3D(double x_lim_min, double x_lim_max,
                    double y_lim_min, double y_lim_max,
                    double z_lim_min, double z_lim_max,
                    double dL = 0.025)
    {
        // Populate the attrubutes of the medium
        _x_lim_min = x_lim_min;
        _x_lim_max = x_lim_max;
        _y_lim_min = y_lim_min;
        _y_lim_max = y_lim_max;
        _z_lim_min = z_lim_min;
        _z_lim_max = z_lim_max;

        // Res variables
        _dL = dL;                      // Populates the spatial step
        _res = (int)Math.Round(1/dL);  // calculatea and stores the gridpoints per meter

        // Finds and stores the dimensions of the medium
        double del_x = x_lim_max - x_lim_min;
        double del_y = y_lim_max - y_lim_min;
        double del_z = z_lim_max - z_lim_min;

        // Finds the gridpoints per direction
        int x_res = (int)Math.Round(del_x * _res);
        int y_res = (int)Math.Round(del_y * _res);
        int z_res = (int)Math.Round(del_z * _res);

        // Initializes 3D parameters
        grid = new double[x_res, y_res, z_res];   // Stores the gridspace of electric potential values
        isFixed = new bool[x_res, y_res, z_res];  // Stores flags for nodes constrained to fixed potential
        rho = new double[x_res, y_res, z_res];    // Stores charge density distribution throughout the medium
    }

    // **************************************** Coordinate to index conversion *****************************************

    // Get the i, j, k index for any given coordinate x, y, z
    public (int, int, int) GetIndex(double x, double y, double z)
    {
        // Converts spatial coordinates into discrete grid indices
        int ix = (int)Math.Round((x - _x_lim_min) * _res);
        int iy = (int)Math.Round((y - _y_lim_min) * _res);
        int iz = (int)Math.Round((z - _z_lim_min) * _res);

        return (ix, iy, iz);
    }

    // Get the x, y, z cooredinates for any given index i, j, k
    public (double, double, double) GetCoords(int i, int j, int k)
    {
        // Converts discrete grid indices back into physical spatial coordinates
        double x = _x_lim_min + i * _dL;
        double y = _y_lim_min + j * _dL;
        double z = _z_lim_min + k * _dL;

        return (x, y, z);
    }

    // ******************************************* Add fixed-potential disk ********************************************
    public void AddDisk(double X, double Y, double Z, double radius, double V)
    {
        // Grab the index of the center of the disk
        var (cx, cy, cz) = GetIndex(X, Y, Z);

        // Converts physical radius into grid index radius
        int rIndex = (int)Math.Round(radius * _res);

        // Find the dimensions of the grid
        int nx = grid.GetLength(0);
        int ny = grid.GetLength(1);
        int nz = grid.GetLength(2);

        // If disk is placed outside valid z-plane, exit
        if (cz < 0 || cz >= nz) return;

        // Iterate through the grid, checking if the grid points fall inside the disk radius
        for (int i = Math.Max(0, cx - rIndex); i <= Math.Min(nx - 1, cx + rIndex); i++)
        {
            for (int j = Math.Max(0, cy - rIndex); j <= Math.Min(ny - 1, cy + rIndex); j++)
            {
                // Compute offset from disk center
                int dx = i - cx;
                int dy = j - cy;

                // Check if point lies within circular cross section of disk
                if (dx * dx + dy * dy <= rIndex * rIndex)
                {
                    // Assign fixed potential value and mark node as constrained
                    grid[i, j, cz] = V;
                    isFixed[i, j, cz] = true;
                }
            }
        }
    }

    // *********************************************** Add point charge ************************************************
    public void AddCharge(double X, double Y, double Z, double charge)
    {
        // Convert spatial coordinate into nearest grid index
        var (cx, cy, cz) = GetIndex(X, Y, Z);

        int nx = grid.GetLength(0);
        int ny = grid.GetLength(1);
        int nz = grid.GetLength(2);

        // If the index is out of bounds, do nothing
        if (cx < 0 || cx >= nx ||
            cy < 0 || cy >= ny ||
            cz < 0 || cz >= nz)
            return;

        // Volume of a single grid cell used to convert charge into charge density
        double cellVolume = Math.Pow(_dL, 3);

        // Adds charge density contribution to the selected grid cell
        rho[cx, cy, cz] += charge / cellVolume;
    }
}
