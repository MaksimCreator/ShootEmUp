using UnityEngine;
using ShootEmUp.Observer;

namespace ShootEmUp.Model
{
    public class EnemySpawnerBullet : BulletSpawner,IService
    {
        private readonly IEnemyBulletConfig _config;

        private Vector2 _positionSpawnBulletCurentEnemy;

        public EnemySpawnerBullet(ServiceLocator locator) : base(locator,locator.GetService<IEnemyBulletObserver>() as BulletObserver)
        {
            _config = SingelServiceLocator.GetService<IEnemyBulletConfig>();
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
