using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public interface IBulletObserver
    {
        void TryAddActionDisable(Bullet bullet, Action<Bullet> disableBulletFromPoolObject);
    }
}