namespace ShootEmUp.Model
{
    public interface IEnemyMovement :IDeltaUpdatable, IService
    {
        void Move(Enemy enemy, float delta);
    }
}
