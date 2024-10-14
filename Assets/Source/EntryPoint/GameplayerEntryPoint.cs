using System.Collections.Generic;
using ShootEmUp.View;
using UnityEngine;
using Zenject;

namespace ShootEmUp.EntryPoint
{
    public class GameplayerEntryPoint : MonoBehaviour
    {
        [SerializeField] private List<PhysicsEventsBroadcaster> _allDisableBulletZone;
        [SerializeField] private List<PhysicsEventsBroadcaster> _allDisableEnemyZone;
        [SerializeField] private GameplayManager _gameplayManager;
        [SerializeField] private PhysicsEventsBroadcaster _playerPhysicsEventsBroadcaster;
        [SerializeField] private TransformableView _playerTransformableView;
        [SerializeField] private EndGamePanel _endGamePanel;
        [SerializeField] private ExitViewPanel _exitViewPanel;
        [SerializeField] private GameplayViewPanel _gameplayView;
        [SerializeField] private TimerViewPanel _timerViewPanel;

        private GameController _gameController;

        [Inject]
        private void Construct(GameController gameController)
        { 
           _gameController = gameController;
        }

        private void OnEnable()
        {
            _gameController.Init(_allDisableBulletZone, _allDisableEnemyZone, _gameplayManager, _playerPhysicsEventsBroadcaster,
               _playerTransformableView, _gameplayView, _exitViewPanel, _endGamePanel, _timerViewPanel, this);

            _gameplayManager.Enable();
        }

        private void OnValidate()
        {
            if(enabled == false)
                enabled = true;
        }
    }
}
