using CSV
using DataFrames
using Plots

# Use a fast backend
gr()

# ===================================================== Load data ======================================================
pos = CSV.read("Projects/Project_6/pos.csv", DataFrame)
rms = CSV.read("Projects/Project_6/rms.csv", DataFrame)

# =================================================== Plot RMS data ====================================================
# Expect columns: step, rms
rename!(rms, [:step, :rms])

# Plot the data
plot(rms.step, rms.rms,
     xlabel = "Step",
     ylabel = "RMS Displacement (meters)",
     title = "Tracking RMS Displacement for Random Walk Diffusion",
     legend = false,
     size=(1200, 800))

savefig("rms.png")
println("Plot saved as rms.png")

# ===================================================== Animation ======================================================
# Expect columns: step, x, y, z
rename!(pos, [:step, :x, :y, :z])

# Unique timesteps
steps = sort(unique(pos.step))

# Iterate each frame
anim = @animate for s in steps
    frame = pos[pos.step .== s, :]

    scatter(
        frame.x,
        frame.y,
        frame.z,
        markersize = 0.1,
        xlims = (-0.75, 0.75),
        ylims = (-0.75, 0.75),
        zlims = (-0.75, 0.75),
        xlabel = "X (meters)",
        ylabel = "Y (meters)",
        zlabel = "Z (meters)",
        title = "Diffusion — Step $s",
        legend = false,
        size=(1200, 800)
    )
end

# =================================================== Save animation ===================================================
gif(anim, "diffusion.mp4", fps = 30)

println("Saved diffusion.mp4")
