using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public class EnemyObserver : IEnemyObserver
    {
        private readonly EnemyDeathAction _enemyDeath;
        private readonly EnemyDisableAction _enemyDisable;
        private readonly EnemyDisableFromZone _enemyDisableFromZone;

        public EnemyObserver(ServiceLocator locator)
        {
            _enemyDeath = locator.GetService<EnemyDeathAction>();
            _enemyDisable = locator.GetService<EnemyDisableAction>();
            _enemyDisableFromZone = locator.GetService<EnemyDisableFromZone>();
        }

        public void TryAddActionEnemyDisable(Enemy enemy, Action<Enemy> disable)
        => _enemyDisable.TryAddAction(enemy,disable);

        public void TryAddActionDeath(Enemy enemy,Action<Enemy> onDeath)
        => _enemyDeath.TryAddAction(enemy,onDeath);

        public void RemoveActionOnDeath(Enemy enemy, Action<Enemy> onDeath)
        => _enemyDeath.RemoveAction(enemy,onDeath);

        public void TryAddActionDisableFromZone(Enemy enemy, Action<Enemy> onDisable)
        => _enemyDisableFromZone.TryAddAction(enemy,onDisable);

        public void RemoveActionOnDisable(Enemy enemy, Action<Enemy> onDisable)
        => _enemyDisable.RemoveAction(enemy,onDisable);

        public void Disable()
        {
            _enemyDeath.Disable();
            _enemyDisable.Disable();
            _enemyDisableFromZone.Disable();
        }

        public void Enable()
        {
            _enemyDeath.Enable();
            _enemyDisable.Enable();
            _enemyDisableFromZone.Enable();
        }
    }
}