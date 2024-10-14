using System;
using UnityEngine.InputSystem;

namespace ShootEmUp.Model
{
    public sealed class InputRouter : IInputService,IInputControl
    {
        private readonly ShipInput _input = new();

        public event Action<float> onMove;
        public event Action onFire;

        public void Enable()
        => _input.Enable();

        public void Disable()
        => _input.Disable();

        public void Update()
        {
            if (IsShot())
                OnShot();

            TryMove();
        }

        private void TryMove()
        {
            float direction = _input.Ship.Move.ReadValue<float>();

            if(direction != 0)
                onMove.Invoke(direction);
        }

        private void OnShot()
        => onFire.Invoke();

        private bool IsShot()
        => _input.Ship.Shot.phase == InputActionPhase.Performed;
    }
}
