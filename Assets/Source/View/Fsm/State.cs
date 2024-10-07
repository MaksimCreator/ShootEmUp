namespace ShootEmUp.View
{
    public abstract class State
    {
        protected Fsm Fsm;

        public State(Fsm fsm)
        {
            Fsm = fsm;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
