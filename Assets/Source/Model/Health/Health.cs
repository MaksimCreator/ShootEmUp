using System;

namespace ShootEmUp.Model
{
    public abstract class Health
    {
        public int MinHealth => 0;
        public int MaxHealth { get; }
        public float CurentHealth { get; private set; }

        public event Action onTakeDamage;
        public event Action onDeath;

        public Health(int maxHealth, float curentHealth)
        {
            MaxHealth = maxHealth;
            CurentHealth = curentHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0)
                throw new InvalidOperationException(nameof(damage));

            if (CurentHealth <= MinHealth)
                throw new InvalidOperationException();

            if (CurentHealth - damage <= 0)
                Death();
            else
                GetDamage(damage);
        }

        protected void Heal()
        => CurentHealth = MaxHealth;

        protected void Death()
        { 
            CurentHealth = 0;
            onTakeDamage?.Invoke();
            onDeath.Invoke();
        }

        private void GetDamage(float damage)
        {
            CurentHealth -= damage;
            onTakeDamage?.Invoke();
        }
    }
}