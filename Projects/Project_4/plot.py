import numpy as np
import matplotlib.pyplot as plt
import pandas as pd

df = pd.read_csv("Projects/Project_4/data.csv")

z_vals = np.sort(df["z"].unique())
z_slice = z_vals[np.argmin(np.abs(z_vals - 0.0))]

slice_df = df[np.isclose(df["z"], z_slice)]

pivot = slice_df.pivot(index="x", columns="y", values="V")

grid = slice_df.pivot(index="x", columns="y", values="V")

X, Y = np.meshgrid(grid.index.values, grid.columns.values, indexing="ij")
V = grid.values

V_plot = np.clip(V, -1, 1)

fig = plt.figure()
ax = fig.add_subplot(111, projection="3d")

surf = ax.plot_surface(
    X, Y, V
)

plt.show()
