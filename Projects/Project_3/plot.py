import pandas as pd
import matplotlib.pyplot as plt

df = pd.read_csv("Projects/Project_3/J=1000Jdata.csv")

# Extract columns
df.columns = df.columns.str.strip()  # removes whitespace

x1 = df['x1']
y1 = df['y1']
z1 = df['z1']

x2 = df['x2']
y2 = df['y2']
z2 = df['z2']

x3 = df['x3']
y3 = df['y3']
z3 = df['z3']

# Create 3D plot
fig = plt.figure(dpi=250, figsize=(5, 4))
ax = fig.add_subplot(111, projection='3d')

ax.plot(x1, y1, z1, label="Sun", color='gold')
ax.plot(x2, y2, z2, label="Earth", color='deepskyblue')
ax.plot(x3, y3, z3, label="Jupiter", color='orange')

# Optional: labels
ax.set_xlabel('X (AU)')
ax.set_ylabel('Y (AU)')
ax.set_zlabel('Z (AU)')
ax.set_title('Three Body orbit')
fig.legend()

plt.show()
#plt.savefig("Projects/Project_3/J=1000_J.png")
