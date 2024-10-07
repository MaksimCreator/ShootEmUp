using System;
using ShootEmUp.Model;

namespace ShootEmUp.Observer
{
    public sealed class CharacterTakeDamageAction : ActionObserver<Character, Action>, IService
    {
        protected override void Subscription(Character chracter, Action action)
        => chracter.onTakeDamage += action;

        protected override void Unsubscribing(Character character, Action action)
        => character.onTakeDamage -= action;
    }
}