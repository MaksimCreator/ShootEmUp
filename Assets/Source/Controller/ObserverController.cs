using ShootEmUp.Model;
using ShootEmUp.Observer;

namespace ShootEmUp.Controller
{
    public sealed class ObserverController : IControl
    {
        private readonly IControl[] _observers;

        public ObserverController(ServiceLocator locator)
        {
            ICharacterBulletObserver characterBulletObserver = locator.GetService<ICharacterBulletObserver>();
            IEnemyBulletObserver enemyBulletObserver = locator.GetService<IEnemyBulletObserver>();
            ICharacterObserver characterObserver = locator.GetService<ICharacterObserver>();
            IEnemyObserver enemyObserver = locator.GetService<IEnemyObserver>();
            IRowObserver rowObserver = locator.GetService<IRowObserver>();

            _observers = new IControl[]
            {
                characterBulletObserver,
                enemyBulletObserver,
                characterObserver,
                enemyObserver,
                rowObserver,
            };
        }

        public void Enable()
        {
            for(int i = 0; i < _observers.Length;i++)
                _observers[i].Enable();
        }

        public void Disable()
        {
            for (int i = 0; i < _observers.Length; i++)
                _observers[i].Disable();
        }
    }
}