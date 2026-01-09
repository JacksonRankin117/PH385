# Chapter 2: Realistic Projectile Motion

This chapter discusses many problems involving air resistance.

## 2.1 Bicycle Racing: The Effect of Air Resistance

Newton's Second Law can be expressed as:

$$\frac{dv}{dt} = \frac{F}{m}$$

Acknowledging all the forces on a bike is exhausting, so what we can do is put the change in energy in terms of power:

$$\frac{dE}{dt} = P$$

If the road is flat, all we deal with is kinetic energy:

$$\frac{dv}{dt} = \frac{P}{mv}$$

If $P$ is a constant, we can solve this analytically:

$$\int_{v_0}^{v} v' \; dv' = \int_{0}^{t} \frac{P}{mv} \; dt'$$

And we can solve for $v$ after integrating:

$$v = \sqrt{v_0^2 + 2Pt/m}$$

But this result is unphysical. Velocity would increase without bound. Lets put our ODE in terms of a finite difference:

$$\frac{dv}{dt} \approx \frac{v_{i+1} - v_i}{\Delta t}$$

Substituting our ODE, we can arrange to find this:

$$v_{i+1} = v_i + \frac{P}{mv_i}\Delta t$$

This is the Euler's method to solving the ODE. This of course neglects friction.

We can write out this force due to drag like this:

$$F_{drag} \approx - B_1 v - B_2 v^2$$

This is Stoke's law, and for most objects, the $B_1$ term is negligible. $B_2$, however cannot be calculated, but we can make the educated guess that when an object flies through the air, it must push a mass of air out of the way at any given time. This can be approximated as $m_{air} ~ \rho A v dt$, so its kinetic energy is $T_{air} = m_{air}v^2/2$, so $F_{drag}vdt = E_{air}$. Putting this together, we get:

$$F_{drag} \approx - \frac{1}{2}C \rho A v^2$$

Where $C$ is the drag coefficient. We can write our Euler's method more completely to include the force from drag:

$$v_{i+1} = v_i + \frac{P}{mv_i}\Delta t - \frac{C \rho A v_i^2}{2m}\Delta t$$

## 2.2 Projectile Motion: The Trajectory of a Cannon Shell

Lets ignore air resistance again. The behavior of the shell can be modeled by these 2nd-order ODEs:

$$\frac{d^2x}{dt^2} = 0$$

$$\frac{d^2y}{dt^2} = -g$$

We can rewrite this to be a total of four first order ODEs:

$$\frac{dx}{dt}=v_x$$

$$\frac{dv_x}{dt}=0$$

$$\frac{dy}{dt}=v_y$$

$$\frac{dv_y}{dt}=-g$$

Which can be rewritten for Euler's method, and we can easily add air resistance terms:

$$x_{i + 1} = x_i + v_{x, i} \Delta t$$

$$v_{x,i + 1} = v_{x,i} + \frac{B_2 v v_{x, i}}{m} \Delta t$$

$$y_{i + 1} = y_i + v_{y, i} \Delta t$$

$$v_{y,i + 1} = v_{y,i} - g\Delta t + \frac{B_2 v v_{y, i}}{m} \Delta t$$

But lets say the density of the air is also a consideration. Well, the easiest approximation is to treat the atmosphere as an isothermal ideal gas. Then the approximation becomes:

$$P(y) = P(0)e^{\frac{-mgy}{k_B T}}$$

And for an ideal gas, pressure is proportional to density, so:

$$\rho(y) = \rho(0) e^{-y/y_0}$$

Where $y_0 = k_B T / mg$, and $rho_0$ is the density at sea level.

If we assume an adiabatic atmosphere:

$$\rho = \rho_0 \left( 1 - \frac{ay}{T_0}\right)^{\alpha}$$

And our drag becomes:

$$F^*_{drag} = \frac{\rho}{\rho_0} F_{drag}(y = 0)$$

## 2.3 Motion of a Batted Ball