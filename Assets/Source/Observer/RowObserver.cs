using ShootEmUp.Model;
using System;

namespace ShootEmUp.Observer
{
    public class RowObserver : IRowObserver
    {
        private readonly RowEndAction _rowEnd;

        public RowObserver(ServiceLocator locator)
        {
            _rowEnd = locator.GetService<RowEndAction>();
        }

        public void AddEndAction(Row row, Action<Row> onEnd)
        => _rowEnd.TryAddAction(row, onEnd);

        public void RemoveEndAction(Row row, Action<Row> onEnd)
        => _rowEnd.RemoveAction(row, onEnd);

        public void Disable()
        => _rowEnd.Disable();

        public void Enable()
        => _rowEnd.Enable();
    }
}
