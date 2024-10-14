using System;
using ShootEmUp.Model;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Controller
{
    public sealed class CharacterController : IDeltaUpdatable, IControl
    {
        private readonly IInputControl _inputControl;
        private readonly IInputService _inputService;
        private readonly ICharacterMovement _characterMovemeng;
        private readonly IBulletSimulated _bulletSimulated;
        private readonly CharacterSpawnerBullet _bulletSpawner;
        private readonly Character _character;

        [Inject]
        public CharacterController(IInputControl inputControl, IInputService inputService, ICharacterMovement characterMovement, IBulletSimulated bulletSimulated, CharacterSpawnerBullet characterSpawnerBullet, Character character)
        {
            _inputControl = inputControl;
            _inputService = inputService;
            _characterMovemeng = characterMovement;
            _bulletSimulated = bulletSimulated;
            _bulletSpawner = characterSpawnerBullet;
            _character = character;
        }

        public void Enable()
        {
            _inputService.onMove += Move;
            _inputService.onFire += Shot;
      
            _inputControl.Enable();
            _character.Enable();
        }

        public void Disable()
        {
            _inputService.onMove -= Move;
            _inputService.onFire -= Shot;
            
            _inputControl.Disable();
            _character.Disable();
        }

        public void Update(float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            _inputService.Update();
        }

        private void Move(float directionX)
        => _characterMovemeng.Move(directionX, Time.deltaTime);

        private void Shot()
        => _bulletSpawner.TrySpawn();
    }
}