using UnityEngine;

namespace ShootEmUp.Model
{
    public interface IBulletConfig
    {
        Color Color { get; }
        int Damage { get; }
        float Speed { get; }
        int Layer { get; }
    }
}