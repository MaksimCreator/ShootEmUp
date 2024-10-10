using System.Collections.Generic;
using ShootEmUp.Controller;
using ShootEmUp.Model;
using ShootEmUp.View;
using UnityEngine;
using System;
using CharacterController = ShootEmUp.Controller.CharacterController;

public class GamaplayMenager : MonoBehaviour
{
    [SerializeField] private GamaplayViewPanel _gameplayPanel;
    [SerializeField] private ExitViewPanel _exitViewPanel;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private TimerViewPanel _timerPanel;
    [SerializeField] private TransformableView _characterTransformableView;
    [SerializeField] private List<PhysicsEventsBroadcaster> _allDisableBulletZone;
    [SerializeField] private List<PhysicsEventsBroadcaster> _allDisableEnemyZone;

    private readonly Fsm _fsm = new();

    private IDeltaUpdatable[] _deltaUpdatable;
    private IControl[] _control;
    private bool _isInitialized;

    public void Initialized(PhysicsEventsBroadcaster player,ServiceLocator locator,CharacterController characterController,EnemysController enemysController,BulletController bulletController,ObserverController observerController)
    {
        if (_isInitialized)
            return;
        
        _deltaUpdatable = new IDeltaUpdatable[]
        {
            characterController,
            enemysController,
            bulletController
        };
        _control = new IControl[]
        {
            characterController,
            enemysController,
            bulletController,
            observerController
        };

        InitializeFSM(locator,player);

        _isInitialized = true;
        enabled = true;
    }

    public void Enable()
    {
        if(enabled == false)
            enabled = true;
    }

    public void Disable() 
    {
        if(enabled)
            enabled = false; 
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            throw new InvalidOperationException();

        for (int i = 0; i < _control.Length; i++)
            _control[i].Enable();

        _fsm.SetState<LevelInitializedState>();
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            throw new InvalidOperationException();

        for (int i = 0; i < _control.Length; i++)
            _control[i].Disable();

        _fsm.SetState<IdelState>();
    }

    private void Update()
    {
        for (int i = 0; i < _deltaUpdatable.Length; i++)
            _deltaUpdatable[i].Update(Time.deltaTime);
    }

    private void InitializeFSM(ServiceLocator locator,PhysicsEventsBroadcaster player)
    {
        _fsm.BindState(new LevelInitializedState(_fsm)).
            BindState(new GUIInitializedState(_fsm, this, locator,_exitViewPanel, _gameplayPanel, _endGamePanel,_timerPanel))
           .BindState(new PhysicsRoutingState(locator,_fsm,_characterTransformableView,player, this, _allDisableBulletZone,_allDisableEnemyZone))
           .BindState(new IdelState(_fsm));
    }
}