namespace ShootEmUp.Model
{
    public interface IEnemyShot : IDeltaUpdatable, IService
    {
        void TryRegistary(Enemy enemy,float timeToShot);
    }
}