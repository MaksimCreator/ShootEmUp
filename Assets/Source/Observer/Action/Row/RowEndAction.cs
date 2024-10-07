using System;
using ShootEmUp.Observer;

namespace ShootEmUp.Model
{
    public sealed class RowEndAction : ActionObserver<Row, Action<Row>>,IService
    {
        protected override void Subscription(Row row, Action<Row> action)
        => row.onEnd += action;

        protected override void Unsubscribing(Row row, Action<Row> action)
        => row.onEnd -= action;
    }
}