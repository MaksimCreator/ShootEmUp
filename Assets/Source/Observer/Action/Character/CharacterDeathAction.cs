using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public sealed class CharacterDeathAction : ActionObserver<Character, Action>, IService
    {
        protected override void Subscription(Character character, Action action)
        => character.onDeath += action;

        protected override void Unsubscribing(Character character, Action action)
        => character.onDeath -= action;
    }
}