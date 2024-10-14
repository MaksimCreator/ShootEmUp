using UnityEngine;
using System;
using Zenject;

namespace ShootEmUp.Model
{
    public class EnemyMovement : Control, IEnemyMovement
    {
        private readonly float _cooldownIncrement;
        private readonly float _speedIncrement;
        private readonly float _maxSpeed;

        private float _speed;
        private float _timer;

        [Inject]
        public EnemyMovement(EnemyMovementConfig config)
        {
            _speed = config.Speed;
            _cooldownIncrement = config.CooldownIncrement;
            _maxSpeed = config.MaxSpeed;
            _speedIncrement = config.SpeedIncrement;

            _timer = _cooldownIncrement;
        }

        public void Update(float delta)
        {
            if (isEnable == false)
                return;

            if (_speed == _maxSpeed)
                return;

            if (delta <= 0)
                throw new InvalidOperationException();

            _timer -= delta;

            if (_timer <= 0)
            {
                _timer = _cooldownIncrement;

                if(_speed + _speedIncrement >= _maxSpeed)
                    _speed = _maxSpeed;
                else
                    _speed += _speedIncrement;
            }
        }

        public void Move(Enemy enemy,float delta)
        {
            if (isEnable == false)
                return;

            if (delta <= 0)
                throw new InvalidOperationException();

            float direction = Vector2.down.y * _speed * delta;
            enemy.MoveY(direction);
        }
    }
}
