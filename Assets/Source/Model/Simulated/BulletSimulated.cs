using System.Collections.Generic;

namespace ShootEmUp.Model
{
    public class BulletSimulated : Simulation<Bullet>,IBulletSimulated
    {
        private readonly Dictionary<Bullet, float> _bulletDirections = new();

        public void Simulated(Bullet bullet, float direction)
        {
            Simulate(bullet);

            if(_bulletDirections.ContainsKey(bullet) == false)
                _bulletDirections.Add(bullet, direction);
        }

        protected override void OnUpdate(float delta)
        {
            foreach (var entity in Entities)
                entity.MoveY(_bulletDirections[entity] * entity.Speed * delta);
        }
    }
}
