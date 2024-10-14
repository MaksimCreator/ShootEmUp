using ShootEmUp.Observer;
using UnityEngine;
using System;
using Zenject;

namespace ShootEmUp.Model
{
    public class CharacterSpawnerBullet : BulletSpawner
    {
        private readonly Character _character;
        private readonly ICharacterBulletConfig _bulletConfig;
        private readonly float _cooldown;

        private float _timer;

        private Vector3 _positionShot => _character.Position;
        private Quaternion _rotationShot => _character.RotationShot;

        [Inject]
        public CharacterSpawnerBullet(Character character,CharacterConfig characterConfig,IBulletSimulated bulletSimulated,ICharacterBulletConfig bulletConfig, ICharacterBulletObserver characterBulletObserver) : base(bulletSimulated,characterBulletObserver as BulletObserver)
        {
            _bulletConfig = bulletConfig;
            _character = character;

            _cooldown = characterConfig.Cooldown;
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
        => TrySpawn(_positionShot, _rotationShot);

        protected override bool CanSpawn()
        => _timer <= 0 && isEnable;

        protected override float GetDireciton()
        => Vector2.up.y;

        protected override void OnSpawn()
        => _timer = _cooldown;

        protected override Bullet GetBullet()
        => new CharacterBullet(_positionShot, _bulletConfig);

        protected override int GetLayer()
        => _bulletConfig.Layer;
    }
}
