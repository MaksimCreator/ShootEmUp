using System;

namespace ShootEmUp.Model
{
    public interface IHealthEvent
    {
        event Action onTakeDamage;
        event Action onDeath;
    }
}
