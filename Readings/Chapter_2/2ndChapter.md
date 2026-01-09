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

In this section, we will implement a more accurate model for drag force. Wind and altitude will also play a role.

Measurements in a wind tunnel also point that the $C$ is a function of $v$. At low speeds, $C$ is around 0.5, but at high speeds, it drops by often more than a factor of 2. This is because at low speeds, the flow around the baseball is more laminar, but at high speeds, the flow becomes turbulent, allowing the baseball to slip more easily through the air.

We can approximate the drag factor with this equation:

$$\frac{B_2}{m} = 0.0039 + \frac{0.0058}{1 + \exp{\left[(v - v_d)/\Delta\right]}}$$

Where $v_d$ = 35 m/s, and $\Delta$ = 5 m/s

The effect of drag is enormous, and taking this into account, we see that the maximum range of balls batted is more around a $35\degree$ angle. The drag force is therefore:

$$F_{drag,x} = -B_2 |\mathbf{v} - \mathbf{v_{wind}}|(v_x - v_{wind})$$

$$F_{drag,y} = -B_2 |\mathbf{v} - \mathbf{v_{wind}}|v_y$$

Where a positive $v_{wind}$ corresponds to a tailwind, propelling the ball forward. However good this model may seem, it is still not adequate to correctly model a true baseball, as there are too many factors involved.

## 2.4 Throwing a Baseball: The Effects of Spin

As a baseball is thrown, the pitcher may impart an angular velocity on the ball. As the ball is thrown with, say, backspin, the wind speed of the top of the ball will be less than the wind speed of the bottom of the ball. As the force of drag is proportional to the square of the velocity, there will be a stronger force on the bottom of the ball rather than the top. This force will have a non-negligible upward component, which will cause the ball to rise as it travels forward. 

To find this force, we need only find the difference between the drag on the top and on the bottom of the ball:

$$F_M \propto (v + r\omega)^2 - (v i r\omega)^2 ~ vr\omega$$

So the general form of the magnus force is 

$$F_M = S_0 \omega v_x$$

Where the coefficient $S_0$ takes care of averaging the drag force over the face of the ball.

To calculate the trajectory of the ball, we must consider three dimensions. Let the $x$ axis be the axis that runs through home plate and the pitcher, $y$ will describe the height, and $z$ will be the axis perpendicular to $x$ and $y$, which runs on the ground.

The equations of motion for a sidearm curve ball are then:

$$\frac{dx}{dt} = v_x$$

$$\frac{dv_x}{dt} = -\frac{B_2}{m} \, v \, v_x$$

$$\frac{dy}{dt} = v_y$$

$$\frac{dv_y}{dt} = -g$$

$$\frac{dv_z}{dt} = v_z$$

$$\frac{dv_z}{dt} = -\frac{S_0\, v_x\, \omega}{m}$$

Where we assume that $\omega$ is parallel to $y$

Next, lets consider the curve ball, where the pitcher imparts a large amount of spin on the ball. We are able to find the lateral force of the ball with an approximation:

$$\frac{F_{\text{lateral}}}{mg} = 0.5\left[\sin(4\theta) - 0.25\sin(8\theta) + 0.08\sin(12\theta) - 0.025\sin(16\theta)\right]$$

## 2.5 Golf

Lets again analyze the motion of the projectile, i.e, the golf ball, in the $xy$ plane. Our equations of motion are therefore:

$$\frac{dv_x}{dt} = -\frac{F_{\text{drag,x}}}{m} - \frac{S_0 \, \omega \, v_y}{m}$$

$$\frac{dv_y}{dt} = -\frac{F_{\text{drag,y}}}{m} - \frac{S_0 \, \omega \, v_x}{m} - g$$

Here we assume that the ball was hit with a backspin, so that $\omega$ is normal to the $xy$ plane. Drag force is:

$$F_{\text{drag}} = -C \, \rho \, A \, v^2$$

At low speeds, $C = 1/2$, and at speeds higher than 14 m/s, $C = 7.0/v$