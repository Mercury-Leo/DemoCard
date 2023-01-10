using _Project.Core;
using _Project.Core.Singleton;

namespace _Project.Game.TurnEffect {
    public class PlayerEffectManager : SingletonBase<PlayerEffectManager> {
        public CoreConventions.CardEffect CurrentEffect { get; private set; }

        public void SetCurrentTurnEffect(CoreConventions.CardEffect effect = CoreConventions.CardEffect.NoEffect) {
            CurrentEffect = effect;
        }
    }
}