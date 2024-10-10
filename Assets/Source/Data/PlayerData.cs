namespace ShootEmUp.Model
{
    public class PlayerData : IService
    {
        private readonly IHealthData _healthData;

        public PlayerData(ServiceLocator locator)
        {
            _healthData = locator.GetService<PlayerHealth>();
        }

        public float MaxHealth => _healthData.MaxHealth;

        public float MinHealth => _healthData.MinHealth;

        public float CurentHealth => _healthData.CurentHealth;
    }
}