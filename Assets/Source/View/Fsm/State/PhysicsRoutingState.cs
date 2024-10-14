using System;
using UnityEngine;
using ShootEmUp.Model;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp.View
{
    public class PhysicsRoutingState : State
    {
        private readonly IPhysicsRouter _router;
        private readonly Character _character;

        private PhysicsEventsBroadcaster _playerPhysicsEventsBroadcaster;
        private List<PhysicsEventsBroadcaster> _allDisableBulletZone;
        private List<PhysicsEventsBroadcaster> _allDisableEnemyZone;
        private TransformableView _playerTransformableView;
        private MonoBehaviour _monoBehaviour;

        private bool _isInit;

        [Inject]
        public PhysicsRoutingState(IGameFsm fsm,IPhysicsRouter physicsRouter,Character character) : base(fsm as Fsm)
        {
            _router = physicsRouter;
            _character = character;
        }

        public void Init(TransformableView playerTransformableView, PhysicsEventsBroadcaster playerPhysicsEventsBroadcaster, MonoBehaviour monoBehaviour,
            List<PhysicsEventsBroadcaster> allDisableBulletZone, List<PhysicsEventsBroadcaster> allDisableEnemyZone)
        {
            _playerPhysicsEventsBroadcaster = playerPhysicsEventsBroadcaster;
            _playerTransformableView = playerTransformableView;
            _allDisableBulletZone = allDisableBulletZone;
            _allDisableEnemyZone = allDisableEnemyZone;
            _monoBehaviour = monoBehaviour;

            _isInit = true;
        }

        public override void Enter()
        {
            if (_isInit == false)
                throw new InvalidOperationException();

            for (int i = 0; i < _allDisableBulletZone.Count; i++)
                _allDisableBulletZone[i].Init(new DisableBulletZone(),_router);

            for (int i = 0; i < _allDisableEnemyZone.Count; i++)
                _allDisableEnemyZone[i].Init(new DisableEnemyZone(), _router);

            _playerPhysicsEventsBroadcaster.Init(_character, _router);
            _playerTransformableView.Init(_character);

            _monoBehaviour.StartCoroutine(GetRouterSteper());
        }

        public override void Exit()
        => _monoBehaviour.StopCoroutine(GetRouterSteper());

        private IEnumerator GetRouterSteper()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                _router.Step();
            }
        }
    }
}
