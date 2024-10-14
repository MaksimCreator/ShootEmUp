using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public sealed class EnemyDisableAction : ActionObserver<Enemy, Action<Enemy>>
    {
        protected override void Subscription(Enemy enemy, Action<Enemy> action)
        => enemy.onDisable += action;

        protected override void Unsubscribing(Enemy entity, Action<Enemy> action)
        => entity.onDisable -= action;
    }
}