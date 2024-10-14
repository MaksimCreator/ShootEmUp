using System;
using Zenject;

namespace ShootEmUp.Model
{
    public class CharacterMovement : ICharacterMovement
    {
        private readonly Character _character;
        private readonly ICharacterRestrictions _restriction;

        private readonly float _speed;

        [Inject]
        public CharacterMovement(ICharacterRestrictions characterRestrictions,Character character, CharacterConfig characterConfig)
        {
            _character = character;
            _restriction = characterRestrictions;

            _speed = characterConfig.Speed;
        }

        public void Move(float directionX, float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException();

            if (directionX == 0)
                return;

            float direction = directionX * delta * _speed;

            if(direction + _character.Position.x <= _restriction.Left)
                direction = _restriction.Left - _character.Position.x;
            else if(direction + _character.Position.x >= _restriction.Right)
                direction = _restriction.Right - _character.Position.x;

            if(direction != 0)
                _character.MoveX(direction);
        }
    }
}
