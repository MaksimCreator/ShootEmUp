using ShootEmUp.Model;
using ShootEmUp.Observer;
using UnityEngine.SceneManagement;

namespace ShootEmUp.View
{
    public class GUIInitializedState : State
    {
        private readonly GamaplayViewPanel _viewPanel;
        private readonly PlayerHealth _playerHealth;
        private readonly GamaplayMenager _gamaplayMenager;
        private readonly EndGamePanel _endGamePanel;
        private readonly PlayerData _playerData;
        private readonly IWallet _wallet;
        private readonly ICharacterObserver _characterObserver;

        public GUIInitializedState(Fsm fsm,ServiceLocator locator, GamaplayViewPanel panel,GamaplayMenager gamaplayMenager,EndGamePanel endGamePanel) : base(fsm)
        {
            _viewPanel = panel;
            _gamaplayMenager = gamaplayMenager;
            _endGamePanel = endGamePanel;
            _wallet = SingelServiceLocator.GetService<IWallet>();
            _characterObserver = locator.GetService<ICharacterObserver>();
            _playerData = locator.GetService<PlayerData>();
            _playerHealth = locator.GetService<PlayerHealth>();
        }

        public override void Enter()
        {
            _characterObserver.TryAddActionOnDeath(Death);
            _viewPanel.Init(_playerData, _characterObserver);
            _viewPanel.Show(GUIExit);

            Fsm.SetState<PhysicsRoutingState>();
        }

        private void Death()
        {
            _gamaplayMenager.Disable();
            _endGamePanel.Show(GUIReset, GUIExit, _wallet.Score);
        }

        private void GUIReset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _wallet.Reset();
        }

        private void GUIExit()
        {
            GameData data = SingelServiceLocator.GetService<GameData>();

            if (data.MaxScore < _wallet.Score)
            {
                data.MaxScore = _wallet.Score;
                SingelServiceLocator.GetService<IDataService>().Save(data);
            }

            _wallet.Reset();
            SceneManager.LoadScene(Constant.StartScena);
        }

    }

}