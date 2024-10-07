using UnityEngine;

namespace ShootEmUp.Model
{
    public abstract class Entity : Transformable
    {
        protected Entity(Vector2 position) : base(position) { }

        public abstract void TakeDamage(float damage);
    }
}
