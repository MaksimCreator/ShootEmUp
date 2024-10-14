namespace ShootEmUp.Model
{
    public class Control : IControl
    {
        protected bool isEnable { get; private set; } = true;

        public virtual void Enable()
        {
            if (isEnable == false)
                isEnable = true;
        }

        public virtual void Disable()
        {
            if (isEnable)
                isEnable = false;
        }
    }
}