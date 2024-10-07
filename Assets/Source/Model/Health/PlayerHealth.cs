namespace ShootEmUp.Model
{
    public class PlayerHealth : Health,IService
    {
        public PlayerHealth(int maxHealth, float curentHealth) : base(maxHealth, curentHealth) { }

        public new void Death()
        => base.Death();
    }
}
