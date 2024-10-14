using System;
using System.Collections.Generic;

namespace ShootEmUp.Model
{
    public abstract class Simulation<T> : Control,IDeltaUpdatable
    {
        private readonly HashSet<T> _entities = new HashSet<T>();

        public IEnumerable<T> Entities => _entities;

        public event Action<T> End;

        public void Update(float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            if (isEnable == false)
                return;

            OnUpdate(delta);
        }

        protected void Simulate(T entity)
        => _entities.Add(entity);

        protected void Stop(T entity)
        => End?.Invoke(entity);

        protected abstract void OnUpdate(float delta);
    }
}
