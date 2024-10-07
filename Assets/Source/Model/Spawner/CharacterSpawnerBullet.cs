using ShootEmUp.Observer;
using UnityEngine;
using System;

namespace ShootEmUp.Model
{
    public class CharacterSpawnerBullet : BulletSpawner,IService
    {
        private readonly Transform _pointSpawn;
        private readonly ICharacterBulletConfig _bulletConfig;
        private readonly float _cooldown;

        private float _timer;

        public CharacterSpawnerBullet(Transform pointSpawn,ServiceLocator locator, float cooldown) : base(locator,locator.GetService<ICharacterBulletObserver>() as BulletObserver)
        {
            _bulletConfig = SingelServiceLocator.GetService<ICharacterBulletConfig>();

            _pointSpawn = pointSpawn;
            _cooldown = cooldown;
        }

        public void Updata(float delta)
        {
            if (isEnable == false)
                return;

            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            if (_timer > 0)
                _timer -= delta;
        }

        public void TrySpawn()
        => TrySpawn(_pointSpawn.position, _pointSpawn.rotation);

        protected override bool CanSpawn()
        => _timer <= 0 && isEnable;

        protected override float GetDireciton()
        => Vector2.up.y;

        protected override void OnSpawn()
        => _timer = _cooldown;

        protected override Bullet GetBullet()
        => new CharacterBullet(_pointSpawn.position, _bulletConfig);

        protected override int GetLayer()
        => _bulletConfig.Layer;
    }
}
