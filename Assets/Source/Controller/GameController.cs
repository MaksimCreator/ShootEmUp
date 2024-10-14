using ShootEmUp.Model;
using ShootEmUp.View;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class GameController : IControl
{
    private GUIInitializedState _GUIInitializedState;
    private PhysicsRoutingState _physicsRoutingState;

    private IGameFsm _fsm;
    private bool _isInit;

    public void Init(List<PhysicsEventsBroadcaster> allDisableBulletZone, List<PhysicsEventsBroadcaster> allDisableEnemyZone, GameplayManager gameplayManager,
        PhysicsEventsBroadcaster player,TransformableView characterTransformableView,GameplayViewPanel gameplayViewPanel,ExitViewPanel exitViewPanel,EndGamePanel endGamePanel,
        TimerViewPanel timerViewPanel,MonoBehaviour monoBehaviour)
    {
        if (_isInit)
            return;

        _GUIInitializedState.Init(gameplayManager, exitViewPanel, gameplayViewPanel, endGamePanel, timerViewPanel);
        _physicsRoutingState.Init(characterTransformableView, player, monoBehaviour, allDisableBulletZone, allDisableEnemyZone);

        _isInit = true;
    }

    public void Disable()
    {
        if (_isInit == false)
            throw new InvalidOperationException();

        _fsm.SetState<IdelState>();
    }

    public void Enable()
    {
        if (_isInit == false)
            throw new InvalidOperationException();

        _fsm.SetState<LevelInitializedState>();
    }

    [Inject]
    private void InitializeFSM(IGameFsm fsm,LevelInitializedState levelInitializedState, GUIInitializedState GUIInitializedState, PhysicsRoutingState physicsRoutingState, IdelState idelState)
    {
        _fsm = fsm;

        _GUIInitializedState = GUIInitializedState;
        _physicsRoutingState = physicsRoutingState;

        _fsm
            .BindState(levelInitializedState)
            .BindState(GUIInitializedState)
            .BindState(physicsRoutingState)
            .BindState(idelState);
    }
}
