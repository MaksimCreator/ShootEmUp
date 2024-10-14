using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Model
{
    public class RowSpawner : Control,IDeltaUpdatable
    {
        private readonly PoolObject<Entity> _defoltEnemyPoolObject;
        private readonly IEnemySpawnPoint _enemyPoints;
        private readonly IEntityCreater _entityCreater;

        private readonly float _cooldownSpawn;

        private float _timer;

        public event Func<Entity,GameObject> onCreat;

        [Inject]
        public RowSpawner(IEntityCreater entityCreater,IEnemySpawnPoint enemyPoints,EnemySpawnerConfig enemySpawnerConfig)
        {
            _defoltEnemyPoolObject = new PoolObject<Entity>();

            _cooldownSpawn = enemySpawnerConfig.CooldownSpawn;

            _entityCreater = entityCreater;
            _enemyPoints = enemyPoints;
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

            _defoltEnemyPoolObject.onCreate += InstantiateEnemy;
        }

        public override void Disable()
        { 
            base.Disable();

            _defoltEnemyPoolObject.onCreate -= InstantiateEnemy;
        }
 
        private void SpawnRow()
        {
            Enemy[] enemys = new Enemy[_enemyPoints.Count];

            for (int i = 0; i < _enemyPoints.Count; i++)
            {
                _defoltEnemyPoolObject.Enable(() => CreatDefoltEnemy(i),_enemyPoints.GetPosition(i),_enemyPoints.GetRotation(i));

                (Entity, GameObject) pair = _defoltEnemyPoolObject.GetLastActivatedObject();

                DefoltEnemy enemy = pair.Item1 as DefoltEnemy;
                GameObject model = pair.Item2;

                enemy.Enable();
                enemy.SetPositin(_enemyPoints.GetPosition(i));
                enemys[i] = enemy;
            }

            _entityCreater.CreatRow(enemys);
        }

        private DefoltEnemy CreatDefoltEnemy(int index)
        => _entityCreater.CreatDefoltEnemy(_enemyPoints.GetPosition(index), _defoltEnemyPoolObject);

        private GameObject InstantiateEnemy(Entity enemy)
        => onCreat.Invoke(enemy);
    }
}
