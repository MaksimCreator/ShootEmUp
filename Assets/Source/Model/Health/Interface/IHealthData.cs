namespace ShootEmUp.Model
{
    public interface IHealthData
    {
        int MaxHealth { get; }
        float CurentHealth { get; }
        int MinHealth { get; }
    }
}
