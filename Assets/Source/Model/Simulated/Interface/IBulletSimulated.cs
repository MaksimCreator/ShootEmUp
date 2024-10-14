namespace ShootEmUp.Model
{
    public interface IBulletSimulated : IControl
    {
        void Simulated(Bullet bullet, float direction);
        void Update(float delta);
    }
}
