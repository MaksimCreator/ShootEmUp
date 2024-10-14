namespace ShootEmUp.Model
{
    public interface IEnemyMovement : IDeltaUpdatable
    {
        void Move(Enemy enemy, float delta);
    }
}
