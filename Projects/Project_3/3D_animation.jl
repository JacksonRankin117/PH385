using CSV
using DataFrames
using CairoMakie
using GeometryBasics
using Makie


# ---------- LOAD DATA ----------
df = CSV.read("Projects/Project_3/data.csv", DataFrame)

# Extract position columns (everything except time)
pos = Tables.matrix(df[:, 2:end])

nframes = nrow(df)
ndims   = size(pos, 2)

@assert ndims % 3 == 0 "Position columns must be multiple of 3"
N = ndims ÷ 3

println("Detected $N bodies, $nframes frames")

# ---------- INITIALIZE POINTS (FRAME 1) ----------
pts_init = Vector{Point3f}(undef, N)
for j in 1:N
    idx = (j - 1) * 3 + 1
    pts_init[j] = Point3f(
        pos[1, idx],
        pos[1, idx + 1],
        pos[1, idx + 2]
    )
end

pts = Observable(pts_init)

# ---------- PLOT ----------
fig = Figure(size = (800, 800), backgroundcolor = :white)
ax  = Axis3(fig[1, 1], 
    title = "N-Body Simulation",
    # Ensure axes and grids are visible
    perspectiveness = 0.5,
    azimuth = 1.27π,
    elevation = 0.15π,
    xticksvisible = true, 
    yticksvisible = true, 
    zticksvisible = true,
    xlabel = "X", ylabel = "Y", zlabel = "Z"
)

scatter!(
    ax,
    pts;
    markersize = 15,
    color = 1:N, # Color particles differently to see motion better
    colormap = :viridis
)

# ---------- CALCULATE GLOBAL BOUNDS ----------
# Flatten all x, y, and z data to find the absolute extremes
all_x = pos[:, 1:3:end]
all_y = pos[:, 2:3:end]
all_z = pos[:, 3:3:end]

# Find the universal min/max across all particles and all time
x_min, x_max = minimum(all_x), maximum(all_x)
y_min, y_max = minimum(all_y), maximum(all_y)
z_min, z_max = minimum(all_z), maximum(all_z)

buffer = 2.0

# Set fixed limits once before recording
limits!(ax, 
    x_min - buffer, x_max + buffer, 
    y_min - buffer, y_max + buffer, 
    z_min - buffer, z_max + buffer
)

# ---------- ANIMATION ----------
step = 100
frames = 1:step:nframes

record(fig, "Projects/Project_3/nbodies_fixed_bounds.mp4", frames; framerate = 60) do i
    # Update the points
    current_pts = [
        Point3f(pos[i, (j-1)*3 + 1], pos[i, (j-1)*3 + 2], pos[i, (j-1)*3 + 3]) 
        for j in 1:N
    ]
    pts[] = current_pts
    # No need for reset_limits!() or limits!() here anymore!
end