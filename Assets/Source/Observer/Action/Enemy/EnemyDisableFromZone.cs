using ShootEmUp.Model;
using System;

namespace ShootEmUp.Observer
{
    public class EnemyDisableFromZone : ActionObserver<Enemy, Action<Enemy>>
    {
        protected override void Subscription(Enemy enemy, Action<Enemy> action)
        => enemy.onDisableFromZone += action;

        protected override void Unsubscribing(Enemy enemy, Action<Enemy> action)
        => enemy.onDisableFromZone -= action;
    }
}