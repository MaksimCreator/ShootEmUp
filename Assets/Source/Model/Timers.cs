using System;
using System.Collections.Generic;

namespace ShootEmUp.Model
{
    public class Timers<T>
    {
        private List<Timer> _timers = new List<Timer>();

        public void Add(T context, float time, Action<T> onEnd,Func<bool> onActive)
        => _timers.Add(new Timer(context, time, onEnd,onActive));

        public void Tick(float deltaTime)
        {
            foreach (var timer in _timers)
            {
                if (timer.OnActive() == false)
                    continue;

                timer.AccumulatedTime += deltaTime;

                if (timer.IsEnd)
                {
                    timer.AccumulatedTime = 0;
                    timer.OnEnd.Invoke(timer.Context);
                }
            }
        }

        private class Timer
        {
            public float AccumulatedTime;
            public readonly float Time;
            public readonly T Context;
            public readonly Action<T> OnEnd;
            public readonly Func<bool> OnActive;

            public bool IsEnd => Time <= AccumulatedTime;

            public Timer(T context, float time, Action<T> onEnd,Func<bool> onActive)
            {
                Time = time;
                Context = context;
                OnEnd = onEnd;
                OnActive = onActive;
            }
        }
    }
}