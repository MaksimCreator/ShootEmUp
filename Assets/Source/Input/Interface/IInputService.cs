using System;

namespace ShootEmUp.Model
{
    public interface IInputService : IUpdatable
    {
        event Action<float> onMove;
        event Action onFire;
    }
}
