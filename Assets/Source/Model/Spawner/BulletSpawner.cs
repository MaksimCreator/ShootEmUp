using System;
using UnityEngine;
using ShootEmUp.Observer;

namespace ShootEmUp.Model
{
    public abstract class BulletSpawner : Control
    {
        private readonly IBulletSimulated _bulletSimulated;
        private readonly PoolObject<Bullet> _poolObject;
        private readonly BulletObserver _bulletObserver;

        public event Func<Bullet,GameObject> onCreate;

        protected BulletSpawner(IBulletSimulated bulletSimulated,BulletObserver bulletObserver)
        {
            _bulletSimulated = bulletSimulated;
            _bulletObserver = bulletObserver;

            _poolObject = new PoolObject<Bullet>();
        }

        public override void Enable()
        {
            base.Enable();
            _poolObject.onCreate += InstantiatBullet;
        }

        public override void Disable()
        {
            base.Disable();
            _poolObject.onCreate -= InstantiatBullet;
        }

        protected void TrySpawn(Vector2 position, Quaternion rotaiton)
        {
            if (CanSpawn())
            {
                OnSpawn();
                _poolObject.Enable(GetBullet,position,rotaiton);

                (Bullet, GameObject) pair = _poolObject.GetLastActivatedObject();

                Bullet bullet = pair.Item1;
                GameObject model = pair.Item2;

                float direction = GetDireciton() > 0 ? 1 : -1;

                bullet.Enable();
                bullet.SetPosition(position);
                model.GetComponent<SpriteRenderer>().color = bullet.Color;
                model.layer = GetLayer();

                _bulletObserver.TryAddActionDisable(bullet, _poolObject.Disable);
                _bulletSimulated.Simulated(bullet, direction);
            }
        }

        protected abstract int GetLayer();

        protected abstract bool CanSpawn();

        protected abstract float GetDireciton();

        protected abstract Bullet GetBullet();

        protected virtual void OnSpawn() { }

        private GameObject InstantiatBullet(Bullet bullet)
        => onCreate.Invoke(bullet);
    }
}