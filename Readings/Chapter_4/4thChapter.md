# Chapter 4: The Solar System

## 4.1 Kepler's Laws

According to Newton, The Universal Law of Gravitation is:

$$F = \frac{G M_S M_E}{r^2}$$

In this case, we are looking at the mass of the Earth and the Sun. Lets assume that the motion of the sun is negligible, i.e. its an immutable point in space

Our equations of motioin are:

$$\frac{d^2x}{dt^2} = \frac{F_{G, x}}{M_E}$$

$$\frac{d^2y}{dt^2} = \frac{F_{G, y}}{M_E}$$

Where 

$$F_{G, x} = -\frac{G M_S M_E}{r^2}\; cos\theta = -\frac{G M_S M_E}{r^3}x$$

So now:

$$\frac{dv_x}{dt} = -\frac{GM_Sx}{r^3}$$

$$\frac{dx}{dt} = v_x$$

$$\frac{dv_y}{dt} = -\frac{GM_Sy}{r^3}$$

$$\frac{dy}{dt} = v_y$$

## 4.2 The Inverse-Square Law and the Stability of Planetary Orbits

How do we know that gravity follows the inverse-square law?

We can plot the planetary trajectory for $F_G \propto 1/r^\beta$, and we see that for $\beta = 2$, the elliptical trajctory traces itself perfectly, and even for subtly variations, like $\beta=2.01$, we see a procession. 

## 4.3 Precession of the Perihelion of Mercury

Mercury and Pluto deviate most from circular orbits. Mercury's orbit actually precesses around the sun. Mercury's orbit makes a full rotation once every 230,000 years

The precession of Mercury could not be explained completely by Netwon's theory of gravity, and it wasn't until Einstein's theory of GR.

According to GR, this new force law is approximately:

$$F_G \approx \frac{G M_E M_S}{r^2} \left( 1 + \frac{\alpha}{r^2}\right)$$

Where $\alpha \approx 1.1 \times 10^{-8} \text{ AU}^2$. Thge rate of precession is given by $C\alpha$, where $C$ is a constant we will calculate. 

## 4.4 The Three-Body Problem and the Effect of Jupiter on Earth

We will need to calculate the force of gravity on the Jupiter-Earth system and the Sun-Earth system, and the Sun-Jupiter system

it turns out its pretty negligible, but its fun to experiment with it