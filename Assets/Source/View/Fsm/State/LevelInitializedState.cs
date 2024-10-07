using UnityEngine;
using ShootEmUp.Model;

namespace ShootEmUp.View
{
    public class LevelInitializedState : State
    {
        public LevelInitializedState(Fsm fsm) : base(fsm)
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
