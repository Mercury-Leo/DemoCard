using System;
using _Project.Core;
using _Project.Core.Singleton;
using Editor.Logger.Scripts;

namespace _Project.Game.TurnEffect {
    public class TurnEffect : SingletonBase<TurnEffect> {
        public (Guid id, CoreConventions.CardEffect effect) CurrentTurn { get; private set; }

        public void SetCurrentEffect(Guid id, CoreConventions.CardEffect effect) {
            CurrentTurn = (id, effect);
            this.LogSuccess($"Current Effect {CurrentTurn.effect}");
        }
    }
}