using UnityEngine;
using ShootEmUp.Observer;
using Zenject;

namespace ShootEmUp.Model
{
    public class EnemySpawnerBullet : BulletSpawner
    {
        private readonly IEnemyBulletConfig _config;

        private Vector2 _positionSpawnBulletCurentEnemy;

        [Inject]
        public EnemySpawnerBullet(IEnemyBulletConfig enemyBulletConfig, IEnemyBulletObserver enemyBulletObserver,IBulletSimulated bulletSimulated) : base(bulletSimulated,enemyBulletObserver as BulletObserver)
        {
            _config = enemyBulletConfig;
        }

        public void Spawn(Enemy enemy)
        {
            _positionSpawnBulletCurentEnemy = enemy.PointSpawnPosition;
            TrySpawn(enemy.PointSpawnPosition, enemy.PointSpawnRotation);
        }

        protected override bool CanSpawn()
        => isEnable;

        protected override float GetDireciton()
        => Vector2.down.y;

        protected override Bullet GetBullet()
        => new EnemyBullet(_positionSpawnBulletCurentEnemy, _config);

        protected override int GetLayer()
        => _config.Layer;
    }
}
