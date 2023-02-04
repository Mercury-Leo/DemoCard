using _Project.Core.Time.TimeProvider.Interfaces;
using _Project.Core.Time.TimeProvider.Scripts;

namespace _Project.Core {
    public static class CoreConventions {
        public const string CardPath = CardBase + CardItem;
        public const string EffectPath = CardBase + Effect;

        const string CardBase = "Card";
        const string Effect = "/Effect";
        const string CardItem = "/Item";

        public const int MaxCardValue = 13;
        public const int MinCardValue = -1;
        public const int DeckSize = 54;

        public static readonly int[] SpecialValues = { -1, -1, 0, 0, 13, 13 };

        public static readonly ITimeProvider TimeProvider = new MonoTimeProvider();
    }
}