using System;
using System.Collections.Generic;

namespace ShootEmUp.Model
{
    public class EnemyShot : Control, IEnemyShot
    {
        private readonly HashSet<Enemy> _enemys = new();
        private readonly Timers<Enemy> _timers = new();
        private readonly EnemySpawnerBullet _spawnerBullet;

        public EnemyShot(ServiceLocator locator)
        {
            _spawnerBullet = locator.GetService<EnemySpawnerBullet>();
        }

        public void TryRegistary(Enemy enemy, float timeToShot)
        { 
            if(_enemys.Add(enemy))
                _timers.Add(enemy, timeToShot, _spawnerBullet.Spawn, () => enemy.isEnable);
        } 

        public void Update(float delta)
        {
            if (isEnable == false)
                return;

            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            _timers.Tick(delta);
        }
    }
}