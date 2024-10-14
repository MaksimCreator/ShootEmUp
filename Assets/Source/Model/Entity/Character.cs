using System;
using UnityEngine;

namespace ShootEmUp.Model
{
    public class Character : Entity
    {
        private readonly PlayerHealth _health;

        public event Action onDeath;
        public event Action onTakeDamage;

        public Quaternion RotationShot => Quaternion.Euler(Vector2.up);

        public Character(PlayerHealth health,Vector2 position) : base(position)
        {
            _health = health;
        }

        public override void Enable()
        {
            base.Enable();

            _health.onDeath += InvokeEventDeath;
            _health.onTakeDamage += InvokeEventTakeDamage;
        }

        public override void Disable()
        {
            base.Disable();

            _health.onDeath += InvokeEventDeath;
            _health.onTakeDamage += InvokeEventTakeDamage;
        }

        public void Death()
        => _health.Death();

        public override void TakeDamage(float damage)
        => _health.TakeDamage(damage);

        private void InvokeEventDeath()
        => onDeath.Invoke();

        private void InvokeEventTakeDamage()
        => onTakeDamage.Invoke();
    }
}
