# Chapter 1: A First Numerical Problem

Many problems in physics can be described using ODEs, like SHOs, projectile motion, and celestial mechanics. Therefore, we shall start with solving 1st-order ODE, and introduce several computational methods in solving them.

## 1.1 Radioactive Decay

${}^{235} \text{U}$ is a typical unstable isotope. It has a low, but not insignificant chance to decay. The best you could to is give a probability for decay. The ODE that describes this behavior for $N_U$ nuclei is as follows:

$$\frac{dN_U}{dt} = -\frac{N_U}{\tau}$$

Where $\tau$ is a time constant. This can be solved analytically:

$$N_U = N_U(0)e^{t/\tau}$$

## 1.2 A Numerical Approach

Lets take the Taylor expansion of $N_U$:

$$N_U(\Delta t) = N_U(0) + \frac{dN_U}{dt} \Delta t + \frac{1}{2} \frac{d^2N_U}{dt^2}(\Delta t)^2 + \cdots,$$

If we take $\Delta t$ to be small, then every term after the second in the expansion can be left out as a good approximation of the result:

$$N_U(\Delta t) \approx N_U(0) + \frac{dN_U}{dt} \Delta t$$

Using the fundamental theorem of calculus will yield the same result:

$$
\frac{dN_U}{dt} \equiv
\lim_{\Delta t \rightarrow 0} \frac{N_U(t + \Delta t) - N_U(t)}{\Delta t} \approx 
\frac{N_U(t + \Delta t) - N_U(t)}{\Delta t}
$$

Where we can arrange to obtain:

$$N_U(t + \Delta t) \approx N_U(t) + \frac{dN_U}{dt} \Delta t ,$$

Inserting $\frac{dN_U}{dt} = -\frac{N_U}{\tau}$, we get:

$$N_U(t + \Delta t) \approx N_U(t) - \frac{N_U}{\tau} \Delta t ,$$

This is known as Euler's Method.

## 1.3 Design and Construction of a Working Program: Codes and Pseudocode

Pseudocode is just a way to express the main program/algorithm work in a common language like English. Essentially, all we want to do is iterate through Euler's method many times to find the approximate number of nuclei. You could go deeper into this, but I am choosing to let this python code speak for itself:

```python
import matplotlib.pyplot as plt

# ==============================================================================
# Initial Conditions
# ==============================================================================

INIT_N    = 1000         # Initial number of nuclei
INIT_TIME = 0            # Initial time

DT        = 0.001        # Step in time (Delta t)
TAU       = 1            # Time constant
MAX_TIME  = 10           # Holds the maximum time before the loop breaks

N         = [INIT_N]     # List that holds the number of U 235 in the sample
t         = [INIT_TIME]  # List that holds the time

# ==============================================================================
# Euler's Method Loop
# ==============================================================================

while t[-1] < MAX_TIME:
    # Calculate the new number of nuclei
    N_Next = N[-1] - N[-1] / TAU * DT

    # Add the new number of nuclei to N_U
    N.append(N_Next)

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

plt.plot()
```

This will yield an image like this:

![Radioactive decay](/Readings/Chapter_1/Decay.png)

## 1.4 Testing Your Program

There are several things you should ask yourself to verify whether or not a program is working properly after debugging it:

*Does the output look reasonable?* Before you make your algorithm, you should know what the output should look like at least roughly. That way you can properly produce good data.

*Does your program agree with any exact results that are available?* We can compare our results with the analytic results in some cases, but mostly not.

*Always check a program gives the same answer for different step sizes* Checking that the step sizes are independent of the algorithm's result is a good test of accuracy. If you notice a large difference in accuracy between two step sizes, your algorithm probably has too high of a step size.

## 1.5 Numerical Considerations

There are numerical errors everywhere in this program. From the finite accuracy of the programming language in question, to the step size in our algorithm. The total error (AKA global error) is proportional to the number of time steps ($t/\Delta t$) and the error per step is ~$(\Delta t)^2$. So a smaller $\Delta t$ introduces less error, which is why in my program, DT = 0.001s

## 1.6 Programming Guidelines and Philosophy

I won't bring details, just a short list:

- Use the `main` program as an overview of your program. Introduce functions and subroutines to detail the structure of your program.
- Use descriptive but succinct words. Prioritize description over word length
- Use comments. Explain logic and describe variables.
- Sacrifice almost everything for clarity. Avoid terse lines that make things less clear/readable. Programs rarely run faster with terse code anyway.
- Take time to make the graphical output clear.