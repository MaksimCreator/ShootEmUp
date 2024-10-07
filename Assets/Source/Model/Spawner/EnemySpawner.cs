using System;
using UnityEngine;
using ShootEmUp.Observer;
using System.Linq;

namespace ShootEmUp.Model
{
    public class EnemySpawner : Control,IDeltaUpdatable,IService
    {
        private const int MinCooldownShot = 3;
        private const int MaxCooldownShot = 12;

        private readonly Transform[] _pointSpawn;
        private readonly EnemyConfig _enemyConfig;

        private readonly RowSimulated _rowSimulated;
        private readonly IRowObserver _rowObserver;

        private readonly IEnemyObserver _enemyObserver;
        private readonly IEnemyMovement _enemyMovemeng;
        private readonly IEnemyShot _enemyShot;

        private readonly PoolObject<Entity> _poolObject;
        private readonly IWallet _wallet;

        private readonly float _cooldownSpawn;

        private float _timer;

        public event Action<Entity,Action<Entity,GameObject>> onSpawn;

        public EnemySpawner(ServiceLocator locator,Transform[] pointSpawn)
        {
            EnemySpawnerConfig config = SingelServiceLocator.GetService<EnemySpawnerConfig>();

            _wallet = SingelServiceLocator.GetService<IWallet>();
            
            _rowSimulated = locator.GetService<RowSimulated>();
            _enemyObserver = locator.GetService<IEnemyObserver>();
            _rowObserver = locator.GetService<IRowObserver>();
            _enemyMovemeng = locator.GetService<IEnemyMovement>();
            _enemyShot = locator.GetService<IEnemyShot>();

            _enemyConfig = config.EnemyConfig;
            _cooldownSpawn = config.CooldownSpawn;

            _poolObject = new PoolObject<Entity>();
            _pointSpawn = pointSpawn;
            _timer = _cooldownSpawn;
        }

        public void Update(float delta)
        {
            if (isEnable == false)
                return;

            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            _timer -= delta;

            if (_timer <= 0)
            {
                _timer = _cooldownSpawn;
                SpawnRow();
            }
        }

        public override void Enable()
        {
            base.Enable();

            _poolObject.onInstantiat += onSpawn;
        }

        public override void Disable()
        { 
            base.Disable();

            _poolObject.onInstantiat -= onSpawn;
        }

        private void SpawnRow()
        {
            Enemy[] enemys = new Enemy[_pointSpawn.Length];

            for (int i = 0; i < _pointSpawn.Length; i++)
            {
                (Entity,GameObject) pair = _poolObject.Enable(CreatDefoltEnemy(_pointSpawn[i].position), _pointSpawn[i].position, _pointSpawn[i].rotation);

                DefoltEnemy enemy = pair.Item1 as DefoltEnemy;
                GameObject model = pair.Item2;

                enemy.Enable();
                enemy.SetPositin(_pointSpawn[i].position);
                enemys[i] = enemy;

                _enemyShot.TryRegistary(enemy, UnityEngine.Random.Range(MinCooldownShot, MaxCooldownShot));
            }

            Row row = CreatRow(enemys);
            _rowSimulated.Simulated(row);
        }

        private DefoltEnemy CreatDefoltEnemy(Vector2 startPosition)
        {
            EnemyHealth health = new EnemyHealth(_enemyConfig.MaxHealth,_enemyConfig.MaxHealth);
            DefoltEnemy enemy = new DefoltEnemy(health, startPosition);

            _enemyObserver.TryAddActionEnemyDisable(enemy, _poolObject.Disable);
            _enemyObserver.TryAddActionDeath(enemy,(enemy) =>
            {
                _wallet.OnKill(enemy);
                enemy.Disable();
            });

            return enemy;
        }

        private Row CreatRow(Enemy[] enemys)
        {
            Row row = new Row(enemys.ToList(), _enemyMovemeng, _enemyObserver);
            _rowObserver.AddEndAction(row,RowDestroy);

            return row;
        }

        private void RowDestroy(Row row)
        {
            _wallet.OnKill(row);
            _rowObserver.RemoveEndAction(row,RowDestroy);
        }
    }
}