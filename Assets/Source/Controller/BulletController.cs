using System;
using ShootEmUp.Model;
using ShootEmUp.View;
using Zenject;

namespace ShootEmUp.Controller
{
    public sealed class BulletController : IDeltaUpdatable, IControl
    {
        private readonly IBulletSimulated _bulletSimulated;
        private readonly EnemySpawnerBullet _enemySpawnerBullet;
        private readonly CharacterSpawnerBullet _characterSpawnerBullet;
        private readonly BulletViewFactroy _bulletViewFactroy;

        private IDeltaUpdatable[] _deltaUpdatable;
        private IControl[] _controller;

        [Inject]
        public BulletController(IBulletSimulated bulletSimulated,CharacterSpawnerBullet characterSpawnerBullet,EnemySpawnerBullet enemySpawnerBullet,BulletViewFactroy bulletViewFactroy)
        {
            _bulletSimulated = bulletSimulated;
            _characterSpawnerBullet = characterSpawnerBullet;
            _enemySpawnerBullet = enemySpawnerBullet;
            _bulletViewFactroy = bulletViewFactroy;
        }

        public void Enable()
        {
            _characterSpawnerBullet.onCreate += _bulletViewFactroy.Create;
            _enemySpawnerBullet.onCreate += _bulletViewFactroy.Create;

            _bulletSimulated.Enable();
            _enemySpawnerBullet.Enable();
            _characterSpawnerBullet.Enable();
        }

        public void Disable()
        {
            _characterSpawnerBullet.onCreate -= _bulletViewFactroy.Create;
            _enemySpawnerBullet.onCreate -= _bulletViewFactroy.Create;

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