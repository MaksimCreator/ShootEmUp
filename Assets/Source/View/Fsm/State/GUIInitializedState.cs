using ShootEmUp.Model;
using ShootEmUp.Observer;
using UnityEngine.SceneManagement;

namespace ShootEmUp.View
{
    public class GUIInitializedState : State
    {
        private readonly GamaplayViewPanel _gameplayViewPanel;
        private readonly ExitViewPanel _exitViewPanel;
        private readonly EndGamePanel _endGamePanel;
        private readonly TimerViewPanel _timerViewPanel;
        private readonly PlayerHealth _playerHealth;
        private readonly GamaplayMenager _gameplayMenager;
        private readonly PlayerData _playerData;
        private readonly IWallet _wallet;
        private readonly ICharacterObserver _characterObserver;

        public GUIInitializedState(Fsm fsm, GamaplayMenager gamaplayMenager, ServiceLocator locator,ExitViewPanel exitViewPanel, GamaplayViewPanel panel,EndGamePanel endGamePanel,TimerViewPanel timerViewPanel) : base(fsm)
        {
            _gameplayMenager = gamaplayMenager;
            _timerViewPanel = timerViewPanel;
            _exitViewPanel = exitViewPanel;
            _endGamePanel = endGamePanel;
            _gameplayViewPanel = panel;

            _wallet = SingelServiceLocator.GetService<IWallet>();

            _characterObserver = locator.GetService<ICharacterObserver>();
            _playerData = locator.GetService<PlayerData>();
            _playerHealth = locator.GetService<PlayerHealth>();
        }

        public override void Enter()
        {
            _characterObserver.TryAddActionOnDeath(Death);
            _gameplayViewPanel.Init(_playerData, _characterObserver);
            EnterGameplayPanel();

            Fsm.SetState<PhysicsRoutingState>();
        }

        private void Death()
        {
            _gameplayMenager.Disable();
            _endGamePanel.Show(Reset, GUIExit, _wallet.Score);
        }

        private void Reset()
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

        private void EnterExitPanel()
        {
            _gameplayMenager.Disable();
            _exitViewPanel.Show(GUIExit, () => _timerViewPanel.Show(EnterGameplay));
        }

        private void EnterGameplayPanel()
        => _gameplayViewPanel.Show(EnterExitPanel);

        private void EnterGameplay()
        {
            _gameplayMenager.Enable();
            EnterGameplayPanel();
        }
    }
}