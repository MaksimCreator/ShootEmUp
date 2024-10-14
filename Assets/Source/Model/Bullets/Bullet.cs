using System;
using UnityEngine;

namespace ShootEmUp.Model
{
    public abstract class Bullet : Transformable
    {
        public readonly int Damage;
        public readonly float Speed;
        public readonly Color Color;

        public event Action<Bullet> onDisable;

        protected Bullet(Vector2 StartPosition,int damage, float speed, Color color) : base(StartPosition)
        {
            Damage = damage;
            Speed = speed;
            Color = color;
        }

        public override void Disable()
        {
            base.Disable();
            onDisable?.Invoke(this);
        }

        public void SetPosition(Vector2 position)
        => Position = position;
    }
}
