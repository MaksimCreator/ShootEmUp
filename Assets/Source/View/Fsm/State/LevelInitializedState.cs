using UnityEngine;
using ShootEmUp.Model;
using Zenject;

namespace ShootEmUp.View
{
    public class LevelInitializedState : State
    {
        [Inject]
        public LevelInitializedState(IGameFsm fsm) : base(fsm as Fsm)
        {

        }

        public override void Enter()
        {
            Physics2D.IgnoreLayerCollision(Constant.LayerEnemy, Constant.LayerEnemy);
            Physics2D.IgnoreLayerCollision(Constant.LayerEnemy, Constant.LayerEnemyBullet);
            Physics2D.IgnoreLayerCollision(Constant.LayerCharacte, Constant.LayerCharacterBullet);

            Fsm.SetState<GUIInitializedState>();
        }
    }
}
