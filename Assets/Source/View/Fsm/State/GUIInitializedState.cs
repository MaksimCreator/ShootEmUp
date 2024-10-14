using ShootEmUp.Model;
using ShootEmUp.Observer;
using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace ShootEmUp.View
{
    public class GUIInitializedState : State
    {
        private readonly PlayerHealth _playerHealth;
        private readonly GameData _gameData;
        private readonly IWallet _wallet;
        private readonly ICharacterObserver _characterObserver;
        private readonly IDataService _dataService;

        private GameplayViewPanel _gameplayViewPanel;
        private ExitViewPanel _exitViewPanel;
        private EndGamePanel _endGamePanel;
        private TimerViewPanel _timerViewPanel;
        private GameplayManager _gameplayMenager;

        private bool _isInit;

        [Inject]
        public GUIInitializedState(IWallet wallet,ICharacterObserver characterObserver,PlayerHealth playerHealth,GameData gameData,
            IGameFsm fsm,IDataService dataService) : base(fsm as Fsm)
        {
            _wallet = wallet;
            _gameData = gameData;
            _characterObserver = characterObserver;
            _playerHealth = playerHealth;
            _dataService = dataService;
        }

        public void Init(GameplayManager gamaplayMenager, ExitViewPanel exitViewPanel, GameplayViewPanel panel, EndGamePanel endGamePanel,TimerViewPanel timerViewPanel)
        {
            _gameplayMenager = gamaplayMenager;
            _timerViewPanel = timerViewPanel;
            _exitViewPanel = exitViewPanel;
            _endGamePanel = endGamePanel;
            _gameplayViewPanel = panel;

            _isInit = true;
        }

        public override void Enter()
        {
            if (_isInit == false)
                throw new InvalidOperationException();

            _characterObserver.TryAddActionOnDeath(Death);
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
            if (_gameData.MaxScore < _wallet.Score)
            {
                _gameData.MaxScore = _wallet.Score;
                _dataService.Save(_gameData);
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