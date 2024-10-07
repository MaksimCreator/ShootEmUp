using ShootEmUp.Model;
using System;

namespace ShootEmUp.Observer
{
    public interface IEnemyObserver : IControl,IService
    {
        void TryAddActionEnemyDisable(Enemy enemy, Action<Enemy> onDisable);
        void TryAddActionDisableFromZone(Enemy enemy, Action<Enemy> onDisable);
        void TryAddActionDeath(Enemy enemy, Action<Enemy> onDeath);
        void RemoveActionOnDeath(Enemy enemy, Action<Enemy> onDeath);
        void RemoveActionOnDisable(Enemy enemy, Action<Enemy> onDisable);
    }
}