using _Project.Core;
using _Project.Core.Singleton;
using Editor.Logger.Scripts;

namespace _Project.Game.TurnEffect {
    public class PlayerEffectManager : SingletonBase<PlayerEffectManager> {
        public CoreConventions.CardEffect CurrentEffect { get; private set; }

        public void SetCurrentTurnEffect(CoreConventions.CardEffect effect) {
            CurrentEffect = effect;
            this.LogSuccess($"Current Effect {CurrentEffect}");
        }
    }
}