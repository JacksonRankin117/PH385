import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

# Load CSV
df = pd.read_csv("data.csv")
df.columns = df.columns.str.strip()  # Clean whitespace from headers

t = df['t']
theta = df['theta']
omega = df['omega']

# Constants for filtering (from project instructions)
driving_omega = 2.0 / 3.0
T = 2 * np.pi / driving_omega

# 1. Time vs. Theta
plt.figure(figsize=(8, 4), dpi=200)
plt.plot(t, theta)
plt.title("Fig 3.6: Time vs. Theta")
plt.xlabel("Time (s)")
plt.ylabel("Theta (rad)")
plt.grid(True)
plt.savefig("Fig_3.6_time_v_theta.png")

# 2. Time vs. Omega
plt.figure(figsize=(8, 4), dpi=200)
plt.plot(t, omega)
plt.title("Time vs. Omega")
plt.xlabel("Time (s)")
plt.ylabel("Omega (rad/s)")
plt.grid(True)
plt.savefig("Fig_3.6_time_v_omega.png")

# 3. Theta vs. Omega (Phase Space)
plt.figure(figsize=(6, 6), dpi=200)
plt.scatter(theta, omega, s=0.3)
plt.title("Fig 3.8: Theta vs. Omega (Phase Space)")
plt.xlabel("Theta (rad)")
plt.ylabel("Omega (rad/s)")
plt.savefig("Fig_3.8_theta_v_omega_full.png")

# 4. Poincaré Section (In-phase with driving force)
# We sample points where t is an integer multiple of the period T
# Using a small epsilon because of floating point precision in the simulation
epsilon = 0.001 
poincare_mask = (t % T) < epsilon

plt.figure(figsize=(6, 6), dpi=200)
plt.scatter(theta[poincare_mask], omega[poincare_mask], s=2)
plt.title("Fig 3.9: Poincaré Section (In-Phase)")
plt.xlabel("Theta (rad)")
plt.ylabel("Omega (rad/s)")
plt.savefig("Fig_3.9_Poincare_Section.png")

#plt.show()