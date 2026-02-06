using System;

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

        // Finds the 
        int x_res = (int)Math.Round(del_x * _res);
        int y_res = (int)Math.Round(del_y * _res);
        int z_res = (int)Math.Round(del_z * _res);

        // Generates 
        grid = new double[x_res, y_res, z_res];
        isFixed = new bool[x_res, y_res, z_res];
        rho = new double[x_res, y_res, z_res];
    }

    // -------------------------------
    // Coordinate ↔ Index conversion
    // -------------------------------

    public (int, int, int) GetIndex(double x, double y, double z)
    {
        int ix = (int)Math.Round((x - _x_lim_min) * _res);
        int iy = (int)Math.Round((y - _y_lim_min) * _res);
        int iz = (int)Math.Round((z - _z_lim_min) * _res);

        return (ix, iy, iz);
    }

    public (double, double, double) GetCoords(int i, int j, int k)
    {
        double x = _x_lim_min + i * _dL;
        double y = _y_lim_min + j * _dL;
        double z = _z_lim_min + k * _dL;

        return (x, y, z);
    }

    // -------------------------------
    // Add fixed-potential disk
    // -------------------------------

    public void AddDisk(double X, double Y, double Z, double radius, double V)
    {
        var (cx, cy, cz) = GetIndex(X, Y, Z);
        int rIndex = (int)Math.Round(radius * _res);

        int nx = grid.GetLength(0);
        int ny = grid.GetLength(1);
        int nz = grid.GetLength(2);

        if (cz < 0 || cz >= nz) return;

        for (int i = Math.Max(0, cx - rIndex); i <= Math.Min(nx - 1, cx + rIndex); i++)
        {
            for (int j = Math.Max(0, cy - rIndex); j <= Math.Min(ny - 1, cy + rIndex); j++)
            {
                int dx = i - cx;
                int dy = j - cy;

                if (dx * dx + dy * dy <= rIndex * rIndex)
                {
                    grid[i, j, cz] = V;
                    isFixed[i, j, cz] = true;
                }
            }
        }
    }

    // -------------------------------
    // Add point charge
    // -------------------------------

    public void AddCharge(double X, double Y, double Z, double charge)
    {
        var (cx, cy, cz) = GetIndex(X, Y, Z);

        int nx = grid.GetLength(0);
        int ny = grid.GetLength(1);
        int nz = grid.GetLength(2);

        if (cx < 0 || cx >= nx ||
            cy < 0 || cy >= ny ||
            cz < 0 || cz >= nz)
            return;

        double cellVolume = Math.Pow(_dL, 3);

        rho[cx, cy, cz] += charge / cellVolume;
    }

    // -------------------------------
    // Optional: Ground boundaries
    // -------------------------------

    public void GroundBoundaries(double V = 0.0)
    {
        int nx = grid.GetLength(0);
        int ny = grid.GetLength(1);
        int nz = grid.GetLength(2);

        for (int i = 0; i < nx; i++)
        for (int j = 0; j < ny; j++)
        {
            SetFixed(i, j, 0, V);
            SetFixed(i, j, nz - 1, V);
        }

        for (int i = 0; i < nx; i++)
        for (int k = 0; k < nz; k++)
        {
            SetFixed(i, 0, k, V);
            SetFixed(i, ny - 1, k, V);
        }

        for (int j = 0; j < ny; j++)
        for (int k = 0; k < nz; k++)
        {
            SetFixed(0, j, k, V);
            SetFixed(nx - 1, j, k, V);
        }
    }

    private void SetFixed(int i, int j, int k, double V)
    {
        grid[i, j, k] = V;
        isFixed[i, j, k] = true;
    }
}
