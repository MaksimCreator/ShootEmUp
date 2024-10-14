using UnityEngine;
using ShootEmUp.Model;
using Zenject;

namespace ShootEmUp.View
{
    public class PhysicsEventsBroadcaster : MonoBehaviour
    {
        private object _model;
        private IPhysicsRouter _router;

        public void Init(object model,IPhysicsRouter physicsRouter)
        {
            _model = model;
            _router = physicsRouter;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        => TryAddCollision(collision.gameObject);

        private void OnTriggerEnter2D(Collider2D collision)
        => TryAddCollision(collision.gameObject);

        private void TryAddCollision(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out PhysicsEventsBroadcaster broadcaster))
                _router.TryAddCollision(_model, broadcaster._model);
        }
    }
}