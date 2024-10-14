using System;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp.Model
{
    public class BulletMovement
    {
        private readonly Dictionary<Bullet, float> _bulletDirections = new();

        [Inject]
        public BulletMovement()
        {

        }

        public void AddDirectionBullet(Bullet bullet, float direction)
        { 
            if(_bulletDirections.ContainsKey(bullet) == false)
                _bulletDirections.Add(bullet, direction);
        }

        public void Move(Bullet bullet,float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException();

            bullet.MoveY(_bulletDirections[bullet] * delta * bullet.Speed);
        }
    }
}
