using Plots
using DelimitedFiles
using FFTW
using Statistics

# 1. Load the data
data_matrix = readdlm("Projects/Project_5/data.csv", ',')

# 2. Physics Constants
L, c, M, r = 1.0, 250.0, 100, 0.01
dt = (r * (L/M)) / c 
fs = 1 / dt     

# 3. FFT Analysis (We use the FULL data for the FFT for better resolution)
mid_node = Int(floor(size(data_matrix, 2) / 2)) + 1 
signal = data_matrix[:, mid_node]
signal_detrended = signal .- mean(signal)

n_fft = length(signal_detrended)
fft_data = abs.(fft(signal_detrended))[1:div(n_fft, 2)]
freqs = range(0, fs / 2, length = div(n_fft, 2))

p1 = plot(freqs, fft_data,
    title = "Power Spectrum (epsilon = 0.00001, k = 10.0)",
    xlabel = "Frequency (Hz)", ylabel = "Power",
    xlim = (0, 1200), lw = 1.2, lc = :red, label = "Node at x=0.5m")
savefig("power_spectrum_eps=0.00001.png")

# 4. Animation - Limited to first 10,000 lines
n_steps_total, n_nodes = size(data_matrix)

# Limit steps to 10,000 or the total available, whichever is smaller
limit = min(n_steps_total, 10000)
x = range(0, 1, length=n_nodes)

# We still want a manageable number of frames (e.g., 200 frames)
# so we skip rows within that 10,000 limit
skip = max(1, div(limit, 200)) 

println("Generating animation for first $limit steps (skip = $skip)...")

anim = @animate for i in 1:skip:limit
    plot(x, data_matrix[i, :], 
        title = "Stiff Damped Cable (First $(limit) steps)",
        xlabel = "Position (m)",
        ylabel = "Displacement (m)",
        ylim = (-0.02, 0.02), 
        lw = 2, lc = :blue,
        label = "t = $(round((i-1)*dt, digits=6))s",
        grid = true)
end

mp4(anim, "cable_vibration_eps=0.00001.mp4", fps = 30)
println("Animation saved as cable_vibration_limited.mp4")