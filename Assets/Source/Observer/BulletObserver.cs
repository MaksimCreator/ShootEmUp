using System;
using ShootEmUp.Model;
using Zenject;

namespace ShootEmUp.Observer
{
    public class BulletObserver : IEnemyBulletObserver,ICharacterBulletObserver
    {
        private readonly BulletDisableAction _bulletDisable;

        [Inject]
        public BulletObserver(BulletDisableAction bulletDisableAction) : base()
        {
            _bulletDisable = bulletDisableAction;
        }

        public void Disable()
        => _bulletDisable.Disable();

        public void Enable()
        => _bulletDisable.Enable();

        public void TryAddActionDisable(Bullet bullet, Action<Bullet> onDisable)
        => _bulletDisable.TryAddAction(bullet, onDisable);
    }
}