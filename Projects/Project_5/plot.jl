using Plots
using DelimitedFiles

# 1. Load the data from your CSV file
# This creates a Matrix where each row is a time step
data_matrix = readdlm("Projects/Project_5/data.csv", ',')

# 2. Setup dimensions
n_steps, n_nodes = size(data_matrix)
x = range(0, 1, length=n_nodes)

# 3. Create the Animation
println("Generating animation frames...")
anim = @animate for i in 1:n_steps
    plot(x, data_matrix[i, :], 
        title = "Stiff Damped Cable Simulation",
        xlabel = "Position (m)",
        ylabel = "Displacement (m)",
        ylim = (-0.1, 0.1),
        lw = 2,
        lc = :blue,
        label = "t = $(round(i * 0.25 / n_steps, digits=4))s",
        grid = true)
end

# 4. Save as a GIF
gif(anim, "cable_vibration.mp4", fps = 30)
println("Animation saved as cable_vibration.gif")