using Zenject;

namespace ShootEmUp.View
{
    public class IdelState : State
    {
        [Inject]
        public IdelState(IGameFsm fsm) : base(fsm as Fsm)
        {
        }
    }
}
