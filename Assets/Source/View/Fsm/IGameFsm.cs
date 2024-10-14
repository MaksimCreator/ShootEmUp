using ShootEmUp.Model;

namespace ShootEmUp.View
{
    public interface IGameFsm : IUpdatable
    {
        void SetState<T>() where T : State;

        Fsm BindState(State state);
    }
}