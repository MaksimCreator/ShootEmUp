using Zenject;

namespace ShootEmUp.Model
{
    public class PlayerData
    {
        private readonly PlayerHealth _healthData;

        [Inject]
        public PlayerData(PlayerHealth playerHealth)
        {
            _healthData = playerHealth;
        }

        public float MaxHealth => _healthData.MaxHealth;

        public float MinHealth => _healthData.MinHealth;

        public float CurentHealth => _healthData.CurentHealth;
    }
}