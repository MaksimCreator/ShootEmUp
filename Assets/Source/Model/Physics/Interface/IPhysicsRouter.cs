namespace ShootEmUp.Model
{
    public interface IPhysicsRouter
    {
        void TryAddCollision(object modelA, object modelB);
        void TryRoute((object, object) pair);
        void Step();
    }
}
