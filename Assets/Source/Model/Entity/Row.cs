using System.Collections.Generic;
using ShootEmUp.Observer;
using System;

namespace ShootEmUp.Model
{
    public class Row
    {
        private readonly IEnemyMovement _enemyMovemeng;
        private readonly IEnemyObserver _enemyObserver;
        private readonly List<Enemy> _enemies;

        public event Action<Row> onEnd;

        private int _numberDeathEnemy;
        private int _countEnemyInRow;

        public Row(List<Enemy> enemies,IEnemyMovement enemyMovemeng,IEnemyObserver observer)
        {
            _enemyMovemeng = enemyMovemeng;
            _enemyObserver = observer;
            _enemies = enemies;

            _countEnemyInRow = enemies.Count;

            for (int i = 0; i < _enemies.Count; i++)
            {
                observer.TryAddActionDeath(_enemies[i],EnemyDeath);
                observer.TryAddActionEnemyDisable(_enemies[i], DisableEnemyToRow);
                observer.TryAddActionDisableFromZone(_enemies[i], (enemy) => observer.RemoveActionOnDeath(enemy,EnemyDeath));
            }
        }

        public void Move(float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].isEnable)
                    _enemyMovemeng.Move(_enemies[i],delta);
            }
        }

        private void EnemyDeath(Enemy enemy)
        {
            _numberDeathEnemy++;

            if (_numberDeathEnemy == _countEnemyInRow)
                onEnd?.Invoke(this);

            _enemyObserver.RemoveActionOnDeath(enemy, EnemyDeath);
        }

        private void DisableEnemyToRow(Enemy enemy)
        {
            _enemyObserver.RemoveActionOnDisable(enemy, DisableEnemyToRow);
            _enemies.Remove(enemy);
        }
    }
}