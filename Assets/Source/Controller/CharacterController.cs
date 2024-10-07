using System;
using ShootEmUp.Model;
using UnityEngine;

namespace ShootEmUp.Controller
{
    public sealed class CharacterController : IDeltaUpdatable, IControl
    {
        private readonly IInputControl _inputControl;
        private readonly IInputService _inputService;
        private readonly ICharacterMovemeng _characterMovemeng;
        private readonly IBulletSimulated _bulletSimulated;
        private readonly CharacterSpawnerBullet _bulletSpawner;
        private readonly Character _character;

        public CharacterController(ServiceLocator locator)
        {
            _inputControl = locator.GetService<IInputControl>();
            _inputService = locator.GetService<IInputService>();
            _characterMovemeng = locator.GetService<ICharacterMovemeng>();
            _bulletSimulated = locator.GetService<IBulletSimulated>();
            _bulletSpawner = locator.GetService<CharacterSpawnerBullet>();
            _character = locator.GetService<Character>();
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