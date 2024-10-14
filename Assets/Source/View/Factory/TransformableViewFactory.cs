using ShootEmUp.Model;
using UnityEngine;
using Zenject;

namespace ShootEmUp.View
{
    public abstract class TransformableViewFactory<T> : MonoBehaviour where T : Transformable
    {
        private IPhysicsRouter _router;

        [Inject]
        private void Construct(IPhysicsRouter physicsRouter)
        {
            _router = physicsRouter;
        }

        public GameObject Create(T entity)
        {
            TransformableView view = Instantiate(GetTemplate(entity), entity.Position, Quaternion.identity);

            if (view.gameObject.TryGetComponent(out PhysicsEventsBroadcaster broadcaster))
                broadcaster.Init(entity,_router);

            view.Init(entity);

            return view.gameObject;
        }

        protected abstract TransformableView GetTemplate(T entity);
    }
}