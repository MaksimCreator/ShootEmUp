using ShootEmUp.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.View
{
    public class PhysicsRoutingState : State
    {
        private readonly PhysicsEventsBroadcaster _playerPhysicsEventsBroadcaster;
        private readonly List<PhysicsEventsBroadcaster> _allDisableBulletZone;
        private readonly List<PhysicsEventsBroadcaster> _allDisableEnemyZone;
        private readonly TransformableView _playerTransformableView;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly IPhysicsRouter _router;
        private readonly Character _character;

        public PhysicsRoutingState(ServiceLocator locator,Fsm fsm,TransformableView playerTransformableView, PhysicsEventsBroadcaster playerPhysicsEventsBroadcaster,MonoBehaviour monoBehaviour,List<PhysicsEventsBroadcaster> allDisableBulletZone,List<PhysicsEventsBroadcaster> allDisableEnemyZone) : base(fsm)
        {
            _playerPhysicsEventsBroadcaster = playerPhysicsEventsBroadcaster;
            _playerTransformableView = playerTransformableView;
            _allDisableBulletZone = allDisableBulletZone;
            _allDisableEnemyZone = allDisableEnemyZone;
            _monoBehaviour = monoBehaviour;
            
            _router = locator.GetService<IPhysicsRouter>();
            _character = locator.GetService<Character>();
        }

        public override void Enter()
        {
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
