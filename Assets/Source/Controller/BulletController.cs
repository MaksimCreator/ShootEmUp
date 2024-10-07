using System;
using ShootEmUp.Model;
using ShootEmUp.View;

namespace ShootEmUp.Controller
{
    public sealed class BulletController : IDeltaUpdatable, IControl
    {
        private readonly IBulletSimulated _bulletSimulated;
        private readonly EnemySpawnerBullet _enemySpawnerBullet;
        private readonly CharacterSpawnerBullet _characterSpawnerBullet;
        private readonly BulletViewFactroy _bulletViewFactroy;

        public BulletController(ServiceLocator locator)
        {
            _bulletSimulated = locator.GetService<IBulletSimulated>();
            _characterSpawnerBullet = locator.GetService<CharacterSpawnerBullet>();
            _enemySpawnerBullet = locator.GetService<EnemySpawnerBullet>();
            _bulletViewFactroy = locator.GetService<BulletViewFactroy>();
        }

        public void Enable()
        {
            _characterSpawnerBullet.onSpawnBullet += _bulletViewFactroy.Create;
            _enemySpawnerBullet.onSpawnBullet += _bulletViewFactroy.Create;

            _bulletSimulated.Enable();
            _enemySpawnerBullet.Enable();
            _characterSpawnerBullet.Enable();
        }

        public void Disable()
        {
            _characterSpawnerBullet.onSpawnBullet -= _bulletViewFactroy.Create;
            _enemySpawnerBullet.onSpawnBullet -= _bulletViewFactroy.Create;

            _bulletSimulated.Disable();
            _enemySpawnerBullet.Disable();
            _characterSpawnerBullet.Disable();
        }

        public void Update(float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            _bulletSimulated.Update(delta);
            _characterSpawnerBullet.Updata(delta);
        }
    }
}