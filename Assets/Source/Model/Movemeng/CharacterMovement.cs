using System;
using UnityEngine;

namespace ShootEmUp.Model
{
    public class CharacterMovement : ICharacterMovemeng
    {
        private readonly float _restrictionsLeftX;
        private readonly float _restrictionsRightX;
        private readonly Character _character;
        private readonly float _speed;

        public CharacterMovement(Vector3 restrictionsLeft, Vector3 restrictionsRight, float speed,ServiceLocator locator)
        {
            _character = locator.GetService<Character>();

            _speed = speed;
            _restrictionsLeftX = restrictionsLeft.x;
            _restrictionsRightX = restrictionsRight.x;
        }

        public void Move(float directionX, float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException();

            if (directionX == 0)
                return;

            float direction = directionX * delta * _speed;

            if(direction + _character.Position.x <= _restrictionsLeftX)
                direction = _restrictionsLeftX - _character.Position.x;
            else if(direction + _character.Position.x >= _restrictionsRightX)
                direction = _restrictionsRightX - _character.Position.x;

            if(direction != 0)
                _character.MoveX(direction);
        }
    }
}
