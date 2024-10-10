using System;
using UnityEngine;

namespace ShootEmUp.Model
{
    public class EnemySpawner : Control,IDeltaUpdatable,IService
    {
        private readonly IEntityCreater _entityCreater;
        private readonly PoolObject<Entity> _poolObject;
        private readonly Transform[] _pointSpawn;

        private readonly float _cooldownSpawn;

        private float _timer;

        public event Action<Entity,Action<Entity,GameObject>> onSpawn;

        public EnemySpawner(ServiceLocator locator,Transform[] pointSpawn)
        {
            _entityCreater = locator.GetService<IEntityCreater>();

            _cooldownSpawn = SingelServiceLocator.GetService<EnemySpawnerConfig>().CooldownSpawn;

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

            _poolObject.onInstantiat += InstantiateEnemy;
        }

        public override void Disable()
        { 
            base.Disable();

            _poolObject.onInstantiat -= InstantiateEnemy;
        }

        private void SpawnRow()
        {
            Enemy[] enemys = new Enemy[_pointSpawn.Length];

            for (int i = 0; i < _pointSpawn.Length; i++)
            {
                (Entity,GameObject) pair = _poolObject.Enable(() => _entityCreater.CreatDefoltEnemy(_pointSpawn[i].position,_poolObject), _pointSpawn[i].position, _pointSpawn[i].rotation);

                DefoltEnemy enemy = pair.Item1 as DefoltEnemy;
                GameObject model = pair.Item2;

                enemy.Enable();
                enemy.SetPositin(_pointSpawn[i].position);
                enemys[i] = enemy;
            }

            _entityCreater.CreatRow(enemys);
        }

        private void InstantiateEnemy(Entity enemy, Action<Entity, GameObject> registary)
        => onSpawn.Invoke(enemy,registary);
    }
}