namespace ShootEmUp.Model
{
    public class CharacterData : ICharacterData
    {
        private readonly IHealthData _healthData;

        public CharacterData(IHealthData healthCharacter)
        {
            _healthData = healthCharacter;
        }

        public int MaxHealth => _healthData.MaxHealth;
        public int MinHealth => _healthData.MinHealth;
        public float CurentHealth => _healthData.CurentHealth;
    }
}