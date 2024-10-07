namespace ShootEmUp.Model
{
    public interface IBulletSimulated : IControl,IService
    {
        void Simulated(Bullet bullet, float direction);
        void Update(float delta);
    }
}
