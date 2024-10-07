using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public class BulletObserver : IEnemyBulletObserver,ICharacterBulletObserver
    {
        private readonly BulletDisableAction _bulletDisable;

        public BulletObserver(ServiceLocator locator) : base()
        {
            _bulletDisable = locator.GetService<BulletDisableAction>();
        }

        public void Disable()
        => _bulletDisable.Disable();

        public void Enable()
        => _bulletDisable.Enable();

        public void TryAddActionDisable(Bullet bullet, Action<Bullet> onDisable)
        => _bulletDisable.TryAddAction(bullet, onDisable);
    }
}