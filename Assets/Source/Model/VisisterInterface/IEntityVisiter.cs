namespace ShootEmUp.Model
{
    public interface IEntityVisiter : IEnemyVisiter
    {
        void Visit(Character character);
    }
}
