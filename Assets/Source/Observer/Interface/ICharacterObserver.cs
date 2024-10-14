using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public interface ICharacterObserver :IControl
    {
        public void TryAddActionOnTakeDamage(Action takeDamage);
        public void TryAddActionOnDeath(Action death);
        public void TryAddActionOnUpdateWallet(Action onUpdate);
    }
}
