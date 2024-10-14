using ShootEmUp.Model;
using ShootEmUp.Observer;
using Zenject;

namespace ShootEmUp.Controller
{
    public sealed class ObserverController : IControl
    {
        private readonly IControl[] _observers;

        [Inject]
        public ObserverController(ICharacterBulletObserver characterBulletObserver,IEnemyBulletObserver enemyBulletObserver,ICharacterObserver characterObserver,IEnemyObserver enemyObserver,IRowObserver rowObserver)
        {
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