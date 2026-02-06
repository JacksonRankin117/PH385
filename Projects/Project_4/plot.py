import numpy as np
import matplotlib.pyplot as plt
import pandas as pd

# Read the csv as a pandas dataframe
df = pd.read_csv("Projects/Project_4/data.csv")

# Only slice through z = 0m
z_vals = np.sort(df["z"].unique())
z_slice = z_vals[np.argmin(np.abs(z_vals - 0.0))]

slice_df = df[np.isclose(df["z"], z_slice)]

pivot = slice_df.pivot(index="x", columns="y", values="V")

grid = slice_df.pivot(index="x", columns="y", values="V")

# Make a meshgrid for potential
X, Y = np.meshgrid(grid.index.values, grid.columns.values, indexing="ij")
V = grid.values

# Plot the meshgrid
fig = plt.figure(figsize=(10, 8), dpi=150)
ax = fig.add_subplot(111, projection="3d")

surf = ax.plot_surface(
    X, Y, V
)

ax.set_title("Electric potential at z=0")
ax.set_xlabel("X (m)")
ax.set_ylabel("Y (m)")
ax.set_zlabel("Electric Potential (V)")

plt.savefig("Projects/Project_4/Potential_Slice_z=0_a=2.0.png")

# plt.show()
