using System.Collections.Generic;
using Zenject;

namespace ShootEmUp.Model
{
    public class BulletSimulated : Simulation<Bullet>,IBulletSimulated
    {
        private readonly BulletMovement _bulletMovement;

        [Inject]
        public BulletSimulated(BulletMovement bulletMovement)
        {
            _bulletMovement = bulletMovement;
        }

        public void Simulated(Bullet bullet, float direction)
        {
            Simulate(bullet);
            _bulletMovement.AddDirectionBullet(bullet, direction);
        }

        protected override void OnUpdate(float delta)
        {
            foreach (var entity in Entities)
                _bulletMovement.Move(entity,delta);
        }
    }
}