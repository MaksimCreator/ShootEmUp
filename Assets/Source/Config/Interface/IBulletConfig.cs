using UnityEngine;

namespace ShootEmUp.Model
{
    public interface IBulletConfig : ISingelService
    {
        Color Color { get; }
        int Damage { get; }
        float Speed { get; }
        int Layer { get; }
    }
}