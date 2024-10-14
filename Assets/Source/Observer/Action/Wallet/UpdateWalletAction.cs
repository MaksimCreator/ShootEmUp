using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public sealed class UpdateWalletAction : ActionObserver<IWallet, Action>
    {
        protected override void Subscription(IWallet entity, Action action)
        => entity.onUpdateScore += action;

        protected override void Unsubscribing(IWallet entity, Action action)
        => entity.onUpdateScore -= action;
    }
}