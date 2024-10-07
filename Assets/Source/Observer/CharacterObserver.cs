using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public class CharacterObserver : ICharacterObserver
    {
        private readonly CharacterDeathAction _deathActions;
        private readonly CharacterTakeDamageAction _takeDamageActions;
        private readonly UpdateWalletAction _updateWalletAction;
        private readonly Character _character;
        private readonly IWallet _wallet;

        public CharacterObserver(ServiceLocator locator)
        {
            _character = locator.GetService<Character>();
            _wallet = SingelServiceLocator.GetService<IWallet>();
            _deathActions = locator.GetService<CharacterDeathAction>();
            _takeDamageActions = locator.GetService<CharacterTakeDamageAction>();
            _updateWalletAction = locator.GetService<UpdateWalletAction>();
        }

        public void Disable()
        {
            _deathActions.Disable();
            _takeDamageActions.Disable();
            _updateWalletAction.Disable();
        }

        public void Enable()
        {
            _deathActions.Enable();
            _takeDamageActions.Enable();
            _updateWalletAction.Enable();
        }

        public void TryAddActionOnDeath(Action onDeath)
        => _deathActions.TryAddAction(_character,onDeath);

        public void TryAddActionOnTakeDamage(Action onTakeDamage)
        => _takeDamageActions.TryAddAction(_character, onTakeDamage);

        public void TryAddActionOnUpdateWallet(Action onUpdate)
        => _updateWalletAction.TryAddAction(_wallet, onUpdate);
    }
}