using System;
using ShootEmUp.Model;
using Zenject;

namespace ShootEmUp.Observer
{
    public class CharacterObserver : ICharacterObserver
    {
        private readonly CharacterDeathAction _deathActions;
        private readonly CharacterTakeDamageAction _takeDamageActions;
        private readonly UpdateWalletAction _updateWalletAction;
        private readonly Character _character;
        private readonly IWallet _wallet;

        [Inject]
        public CharacterObserver(Character character,IWallet wallet, CharacterDeathAction deathAction, CharacterTakeDamageAction takeDamageActions, UpdateWalletAction updateWalletAction)
        {
            _character = character;
            _wallet = wallet;
            _deathActions = deathAction;
            _takeDamageActions = takeDamageActions;
            _updateWalletAction = updateWalletAction;
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