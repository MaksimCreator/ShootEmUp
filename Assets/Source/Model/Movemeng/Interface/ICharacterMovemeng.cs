namespace ShootEmUp.Model
{
    public interface ICharacterMovemeng : IService
    {
        void Move(float directionX, float delta);
    }
}
