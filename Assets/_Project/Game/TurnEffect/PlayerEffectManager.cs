using _Project.Core.Enums.CardEffects;
using _Project.Core.Singleton;

namespace _Project.Game.TurnEffect {
    public class PlayerEffectManager : SingletonBase<PlayerEffectManager> {
        public CardEffects CurrentEffect { get; private set; }

        public void SetCurrentTurnEffect(CardEffects effect = CardEffects.NoEffect) {
            CurrentEffect = effect;
        }
    }
}