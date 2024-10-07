using UnityEngine;

namespace ShootEmUp.View
{
    using Model;
    using System;

    public abstract class TransformableViewFactory<T> : MonoBehaviour, IService where T : Transformable
    {
        private IPhysicsRouter _router;

        public void Init(ServiceLocator locator)
        => _router = locator.GetService<IPhysicsRouter>();

        public void Create(T entity,Action<T,GameObject> registaryEntity = null)
        {
            TransformableView view = Instantiate(GetTemplate(entity), entity.Position, Quaternion.identity);

            if (view.gameObject.TryGetComponent(out PhysicsEventsBroadcaster broadcaster))
                broadcaster.Init(entity, _router);

            if (registaryEntity != null)
                registaryEntity(entity,view.gameObject);

            view.Init(entity);
        }

        protected abstract TransformableView GetTemplate(T entity);
    }
}