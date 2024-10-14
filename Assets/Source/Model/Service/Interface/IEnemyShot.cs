namespace ShootEmUp.Model
{
    public interface IEnemyShot : IDeltaUpdatable
    {
        void TryRegistary(Enemy enemy,float timeToShot);
    }
}