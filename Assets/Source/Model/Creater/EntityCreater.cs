using UnityEngine;
using ShootEmUp.Observer;
using System.Linq;
using Zenject;

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

        [Inject]
        public EntityCreater(IWallet wallet,RowSimulated rowSimulated,IEnemyObserver enemyObserver,IRowObserver rowObserver
            ,IEnemyMovement enemyMovement,IEnemyShot enemyShot,EnemySpawnerConfig enemySpawnerConfig)
        {
            _wallet = wallet;
            _rowSimulated = rowSimulated;
            _enemyObserver = enemyObserver;
            _rowObserver = rowObserver;
            _enemyMovemeng = enemyMovement;
            _enemyShot = enemyShot;
            _enemyConfig = enemySpawnerConfig.EnemyConfig;
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