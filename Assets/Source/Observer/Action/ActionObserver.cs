using System;
using ShootEmUp.Model;
using System.Collections.Generic;

namespace ShootEmUp.Observer
{
    public abstract class ActionObserver<TEntity, TAction> : IControl where TEntity : class where TAction : Delegate
    {
        private readonly Dictionary<TEntity, EntityAction> _entityAction = new();

        public void Disable()
        {
            foreach (var entityAction in _entityAction.Values)
                entityAction.Disable();
        }

        public void Enable()
        {
            foreach (var entityAction in _entityAction.Values)
                entityAction.Enable();
        }

        public void RemoveAction(TEntity entity, TAction action)
        => _entityAction[entity].Remove(action);

        public void TryAddAction(TEntity entity, TAction action)
        { 
            TryRegistaryEntity(entity);
            _entityAction[entity].TryAdd(action);
        }

        protected abstract void Subscription(TEntity entity, TAction action);

        protected abstract void Unsubscribing(TEntity entity,TAction action);
        
        private void TryRegistaryEntity(TEntity entity)
        => _entityAction.TryAdd(entity, new EntityAction(entity, Subscription, Unsubscribing));

        private class EntityAction : IControl
        {
            private readonly TEntity _entity; 
            private readonly List<TAction> _actions = new();
            private readonly List<(TAction,bool)> _isEnable = new();

            private readonly Action<TEntity,TAction> _subscription;
            private readonly Action<TEntity,TAction> _unsubscribing;

            public EntityAction(TEntity entity,Action<TEntity,TAction> subscription, Action<TEntity,TAction> unsubscribing)
            {
                _entity = entity;
                _subscription = subscription;
                _unsubscribing = unsubscribing;
            }

            public void TryAdd(TAction action)
            {
                if (CanRegistary(action) == false)
                    return;

                _actions.Add(action);
                _isEnable.Add((action, false));

                Subscription(action);
            }

            public void Remove(TAction action)
            {
                if (CanRegistary(action))
                    return;

                Unsubscribing(action);

                _actions.Remove(action);
                _isEnable.Remove((action, false));
            }

            public void Disable()
            {
                if (_isEnable.Count != _actions.Count)
                    throw new InvalidOperationException();

                for (int i = 0; i < _actions.Count; i++)
                {
                    if (IsUnsubscribing(i) == false)
                        continue;

                    Unsubscribing(_actions[i]);
                }
            }

            public void Enable()
            {
                if (_isEnable.Count != _actions.Count)
                    throw new InvalidOperationException();

                for (int i = 0; i < _actions.Count; i++)
                {
                    if (IsSubscription(i) == false)
                        continue;

                    Subscription(_actions[i]);
                }
            }

            private bool CanRegistary(TAction action)
            {
                for (int i = 0; i < _actions.Count; i++)
                {
                    if (_actions[i].Equals(action))
                        return false;
                }

                return true;
            }

            private int GetIndex(TAction action)
            {
                for (int i = 0; i < _actions.Count; i++)
                {
                    if (_actions[i].Equals(action))
                        return i;
                }

                throw new ArgumentOutOfRangeException(nameof(action));
            }

            private void SetActive(int indexAction, bool value)
            => _isEnable[indexAction] = (_isEnable[indexAction].Item1, value);

            private void Unsubscribing(TAction action)
            {
                if (IsUnsubscribing(GetIndex(action)) == false)
                    throw new InvalidOperationException();

                _unsubscribing.Invoke(_entity, action);
                SetActive(GetIndex(action), false);
            }

            private void Subscription(TAction action)
            {
                if (IsSubscription(GetIndex(action)) == false)
                    throw new InvalidOperationException();

                _subscription.Invoke(_entity, action);
                SetActive(GetIndex(action), true);
            }

            private bool IsSubscription(int index)
            => _isEnable[index].Item2 == false;

            private bool IsUnsubscribing(int index)
            => _isEnable[index].Item2;
        }
    }
}