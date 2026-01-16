# Chapter 3

Oscillators are ubiquitous in physics, the pendulum probably being the pendulum, or a mass on a string. But what is not so simple, is a damped, driven, large-angle pendulum. This more realistic pendulum exhibits behaviors not seen by a simple harmonic oscillator, i.e. chaotic motion.

## 3.1 Simple Harmonic Motion

Lets assume only gravity and the tension from the string has any affect on the pendulum mass. So the force on the pendulum could be:

$$F_{\theta} = -mg \sin\theta$$

Newton's 2nd law says that this is equal to the mass of the pendulum multiplied by the acceleration. We will also make the assumption that we are dealing with small angles, so $\sin \theta \approx \theta$.

$$\frac{d^2 \theta}{dt^2} = -\frac{g}{l} \theta$$

And thus the solution is:

$$\theta = \theta_0 \sin \left( \Omega t + \phi\right)$$

Lets consider a numerical approach:

$$\frac{d \omega}{dt} = -\frac{g}{l} \theta$$

$$\frac{d\theta}{dt} = \omega$$

Which can be easily translated to Euler's method or similar.

But there is a hitch. If you remember in PH 135, Jackson, where you did this exact same thing, the pendulum motion actually grew. Thus we need a new computational method, Euler-Cromer. The only change we do is instead of using a one-iteration old value of $\omega$, we use the current, recently calculated value of $\omega$. This is a very stable method, and will remain stable for many iterations too, especially at low $\Delta t$

## 3.2 Making the Pendulum More Interesting: Adding Dissipation, Nonlinearity, and a Driving Force

Often the force due to friction is proportional to velocity, so our ODE might look like:

$$\frac{d^2 \theta}{dt^2} = -\frac{g}{l} \theta - q \frac{d\theta}{dt}$$

We can also solve this analytically:

$$\theta(t) = \theta_0 e^{-qt/2} \sin\left( \sqrt{\Omega^2 - q^2/4}t + \phi\right)$$

But this is the case of under damping, where the friction force is sufficiently small to see oscillatory nature.

In the case of over damping, our solution becomes:

$$\theta(t) = \theta_0 e^{-q/2 \left( \sqrt{\Omega^2 - q^2/4} \right)t}$$

In the case of critical damping:

$$\theta(t) = (\theta_0 + Ct) e^{-qt/2}$$

Lets add a sinusoidal driving force. Our equation of motion is:

$$\frac{d^2 \theta}{dt^2} = -\frac{g}{l} \theta - q \frac{d\theta}{dt} + F_D \sin(\Omega_D t)$$

This driving force will compete with the natural frequency of the pendulum. This, can also be solved analytically:

$$\theta(t) = \theta_0 \sin(\Omega_D t + \phi)$$

And the amplitude is:

$$\theta_0 = \frac{F_D}{\sqrt{(\Omega^2 - \Omega_D^2)^2 + (q\Omega_D)^2}}$$

## 3.3 Chaos in the Driven Nonlinear Pendulum

Lets now consider an equation of motion for a large angle pendulum with friction and a driving force:

$$\frac{d^2 \theta}{dt^2} = -\frac{g}{l} \sin\theta - q \frac{d\theta}{dt} + F_D \sin(\Omega_D t)$$

Which we can write as a system of first order ODEs:

$$\frac{d \omega}{dt} = -\frac{g}{l} \sin\theta - q \frac{d\theta}{dt} + F_D \sin(\Omega_D t)$$

$$\frac{d \theta}{dt} = \omega$$

Which can be simulated with Euler-Cromer, or RK4, or Verlet. Except if $\theta$ is outside $[-\pi, \pi]$, we need to clamp it by adding or subtracting $2\pi$