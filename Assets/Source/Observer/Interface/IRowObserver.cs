using ShootEmUp.Model;
using System;

namespace ShootEmUp.Observer
{
    public interface IRowObserver : IControl
    {
        void AddEndAction(Row row, Action<Row> onEnd);
        void RemoveEndAction(Row row, Action<Row> onEnd);
    }
}
