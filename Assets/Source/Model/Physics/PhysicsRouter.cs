using System;
using System.Collections.Generic;
using System.Linq;

namespace ShootEmUp.Model
{
    public class PhysicsRouter : IPhysicsRouter
    {
        private Collisions _collisions = new Collisions();

        private readonly Func<IEnumerable<Record>> _recordsProvider;

        public PhysicsRouter(Func<IEnumerable<Record>> recordsProvider)
        {
            _recordsProvider = recordsProvider;
        }

        public void TryAddCollision(object modelA, object modelB)
        {
            _collisions.TryBind(modelA, modelB);
        }

        public void Step()
        {
            foreach (var pair in _collisions.Pairs)
                TryRoute(pair);

            _collisions = new Collisions();
        }

        public void TryRoute((object, object) pair)
        {
            IEnumerable<Record> records = _recordsProvider?.Invoke().Where(record => record.IsTarget(pair));

            foreach (var record in records)
                record.Do(pair);
        }

        public abstract class Record
        {
            public abstract bool IsTarget((object, object) pair);
            public abstract void Do((object, object) pair);
        }

        public sealed class Record<T1, T2> : Record
        {
            private readonly Action<T1, T2> Action;

            public Record(Action<T1, T2> action)
            {
                Action = action;
            }

            public override void Do((object, object) pair)
            {
                if (pair.Item1 is T1 firstItemT1 && pair.Item2 is T2 firstItemT2)
                {
                    Action(firstItemT1,firstItemT2);
                    return;
                }

                if (pair.Item1 is T2 secondItemT2 && pair.Item2 is T1 secondItemT1)
                {
                    Action(secondItemT1, secondItemT2);
                    return;
                }

                throw new InvalidOperationException();
            }

            public override bool IsTarget((object, object) pair)
            {
                if (pair.Item1 is T1 && pair.Item2 is T2)
                    return true;

                if (pair.Item1 is T2 && pair.Item2 is T1)
                    return true;

                return false;
            }
        }

        private class Collisions
        {
            private List<(object, object)> _pairs = new List<(object, object)>();

            public IEnumerable<(object, object)> Pairs => _pairs;

            public void TryBind(object a, object b)
            {
                foreach (var (left, right) in _pairs)
                {
                    if (left == a && right == b)
                        return;

                    if (left == b && right == a)
                        return;
                }

                _pairs.Add((a, b));
            }
        }
    }
}
