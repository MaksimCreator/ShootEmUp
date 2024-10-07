using UnityEngine;
using System;

namespace ShootEmUp.Model
{
    public abstract class Enemy : Entity
    {
        private const float OffsetY = 0.2f;

        private readonly EnemyHealth _health;

        public Vector3 PointSpawnPosition => Vector3.down * OffsetY + Position;
        public Quaternion PointSpawnRotation => Quaternion.Euler(Vector3.zero);

        public event Action<Enemy> onDeath;
        public event Action<Enemy> onDisable;
        public event Action<Enemy> onDisableFromZone;

        protected Enemy(EnemyHealth health,Vector2 position) : base(position) 
        {
            _health = health;
        }

        public override void TakeDamage(float damage)
        => _health.TakeDamage(damage);

        public override void Enable()
        {
            base.Enable();
            _health.onDeath += Death;
        }

        public override void Disable()
        {
            base.Disable();
            _health.onDeath -= Death;
            onDisable.Invoke(this);
        }

        public void DisableFromZone()
        {
            onDisableFromZone.Invoke(this);
            Disable();
        }

        public void SetPositin(Vector2 positin)
        => Position = positin;

        private void Death()
        { 
            onDeath.Invoke(this);
            _health.Heal();
        }
    }
}