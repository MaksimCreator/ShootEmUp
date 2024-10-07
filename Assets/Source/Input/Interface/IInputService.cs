using System;

namespace ShootEmUp.Model
{
    public interface IInputService : IService,IUpdatable
    {
        event Action<float> onMove;
        event Action onFire;
    }
}
