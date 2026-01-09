import matplotlib.pyplot as plt

# ==============================================================================
# Initial Conditions
# ==============================================================================

INIT_N    = 1000         # Initial number of nuclei
INIT_TIME = 0            # Initial time

DT        = 0.001        # Step in time (Delta t)
TAU       = 1            # Time constant
MAX_TIME  = 10           # Holds the maximum time before the loop breaks

N_U       = [INIT_N]     # List that holds the number of U 235 in the sample
t         = [INIT_TIME]  # List that holds the time

# ==============================================================================
# Euler's Method Loop
# ==============================================================================

while t[-1] < MAX_TIME:
    # Calculate the new number of nuclei
    N_Next = N_U[-1] - N_U[-1] / TAU * DT

    # Add the new number of nuclei to N_U
    N_U.append(N_Next)

    # Advance the time
    t.append(t[-1] + DT)

# ==============================================================================
# Plot Results
# ==============================================================================

plt.figure(figsize=(10, 8), dpi=200)
plt.plot(t, N_U)
plt.title(r"Radioactive decay: $N_0 = 1000,\ \tau = 1\,\mathrm{s}$")
plt.xlabel("Time (s)")
plt.ylabel("Number of Nuclei")
plt.grid(True)

#plt.plot()
plt.savefig("Decay.png")
