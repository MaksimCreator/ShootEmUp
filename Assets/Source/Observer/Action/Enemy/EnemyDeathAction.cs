using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public class EnemyDeathAction : ActionObserver<Enemy, Action<Enemy>>
    {
        protected override void Subscription(Enemy enemy, Action<Enemy> action)
        => enemy.onDeath += action;

        protected override void Unsubscribing(Enemy enemy, Action<Enemy> action)
        => enemy.onDeath -= action;
    }
}