using UnityEngine;
using ShootEmUp.Observer;
using System.Linq;

namespace ShootEmUp.Model
{
    public class EntityCreater : IEntityCreater
    {
        private const int MinCooldownShot = 3;
        private const int MaxCooldownShot = 12;

        private readonly RowSimulated _rowSimulated;
        private readonly IRowObserver _rowObserver;

        private readonly IEnemyObserver _enemyObserver;
        private readonly IEnemyMovement _enemyMovemeng;
        private readonly IEnemyShot _enemyShot;

        private readonly IWallet _wallet;
        private readonly EnemyConfig _enemyConfig;

        public EntityCreater(ServiceLocator locator)
        {
            _wallet = SingelServiceLocator.GetService<IWallet>();

            _rowSimulated = locator.GetService<RowSimulated>();
            _enemyObserver = locator.GetService<IEnemyObserver>();
            _rowObserver = locator.GetService<IRowObserver>();
            _enemyMovemeng = locator.GetService<IEnemyMovement>();
            _enemyShot = locator.GetService<IEnemyShot>();

            _enemyConfig = SingelServiceLocator.GetService<EnemySpawnerConfig>().EnemyConfig;
        }

        public DefoltEnemy CreatDefoltEnemy(Vector2 startPosition,PoolObject<Entity> poolObject)
        {
            EnemyHealth health = new EnemyHealth(_enemyConfig.MaxHealth, _enemyConfig.MaxHealth);
            DefoltEnemy enemy = new DefoltEnemy(health, startPosition);

            _enemyObserver.TryAddActionEnemyDisable(enemy, poolObject.Disable);
            _enemyObserver.TryAddActionDeath(enemy, (enemy) =>
            {
                _wallet.OnKill(enemy);
                enemy.Disable();
            });

            _enemyShot.TryRegistary(enemy, UnityEngine.Random.Range(MinCooldownShot, MaxCooldownShot));

            return enemy;
        }

        public void CreatRow(Enemy[] enemys)
        {
            Row row = new Row(enemys.ToList(), _enemyMovemeng, _enemyObserver);
            _rowObserver.AddEndAction(row, RowDestroy);
            _rowSimulated.Simulated(row);
        }

        private void RowDestroy(Row row)
        {
            _wallet.OnKill(row);
            _rowObserver.RemoveEndAction(row, RowDestroy);
        }
    }
}