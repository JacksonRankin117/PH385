using System.Linq;
class Methods
{
    public static (double, double, double) EulerCromer(Pendulum pend, double dt, double t_f)
    {
        while (pend._times.Last() < t_f)
        {
            pend._omegas.Add(pend._omegas.Last() + dt * (pend.GravityAcceleration(pend._thetas.Last())
                                                      +  pend.FrictionAcceleration(pend._omegas.Last())
                                                      +  pend.DrivingAcceleration(pend._times.Last())));
            
            pend._thetas.Add(pend._thetas.Last() + pend._omegas.Add()* dt);
        }
    }
}