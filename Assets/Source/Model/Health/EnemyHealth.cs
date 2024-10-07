namespace ShootEmUp.Model
{
    public class EnemyHealth : Health
    {
        public EnemyHealth(int maxHealth, float curentHealth) : base(maxHealth, curentHealth)
        {

        }

        public new void Heal()
        => base.Heal();
    }
}
