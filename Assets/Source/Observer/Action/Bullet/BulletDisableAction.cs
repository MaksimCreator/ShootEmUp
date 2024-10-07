using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public sealed class BulletDisableAction : ActionObserver<Bullet,Action<Bullet>>,IService
    {
        protected override void Subscription(Bullet bullet, Action<Bullet> action)
        => bullet.onDisable += action;

        protected override void Unsubscribing(Bullet bullet, Action<Bullet> action)
        => bullet.onDisable -= action;
    }
}