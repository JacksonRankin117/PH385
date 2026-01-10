import pandas as pd
import matplotlib.pyplot as plt

# Load CSV
df = pd.read_csv("Projects/Project_1/data.csv")

# Clean column names
df.columns = df.columns.str.strip()

# Remove unnamed/empty columns
df = df.loc[:, ~df.columns.str.contains('^Unnamed')]

# Check columns
print(df.columns)

# Extract numeric data
x = pd.to_numeric(df['x'], errors='coerce')
y = pd.to_numeric(df['y'], errors='coerce')
z = pd.to_numeric(df['z'], errors='coerce')

# 3D plot
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')
ax.plot(x, y, z)

ax.set_xlabel('X')
ax.set_ylabel('Y')
ax.set_zlabel('Z')
ax.set_title('3D Trajectory')
plt.show()
