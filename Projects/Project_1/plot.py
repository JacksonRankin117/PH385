import pandas as pd
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

# Load CSV
df = pd.read_csv("Projects/Project_1/data.csv")

# Extract columns
df.columns = df.columns.str.strip()  # removes whitespace

x = df['x']
y = df['y']
z = df['z']

# Create 3D plot
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

ax.plot(x, y, z)  # use plot for line

# Optional: labels
ax.set_xlabel('X')
ax.set_ylabel('Y')
ax.set_zlabel('Z')
ax.set_title('3D Trajectory')

plt.show()
