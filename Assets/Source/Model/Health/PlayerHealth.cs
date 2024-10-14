using Zenject;

namespace ShootEmUp.Model
{
    public class PlayerHealth : Health
    {

        public PlayerHealth(CharacterConfig characterConfig) : base(characterConfig.MaxHealth, characterConfig.MaxHealth) { }

        public new void Death()
        => base.Death();
    }
}
