using System.Collections.Generic;
using static _Project.Core.CoreConventions;

namespace _Project.Core.Extensions.Card {
    public static class CardExtensions {
        static readonly Dictionary<int, CardEffect> Cards = new Dictionary<int, CardEffect>() {
            { -1, CardEffect.NoEffect },
            { 0, CardEffect.NoEffect },
            { 1, CardEffect.NoEffect },
            { 2, CardEffect.NoEffect },
            { 3, CardEffect.NoEffect },
            { 4, CardEffect.NoEffect },
            { 5, CardEffect.NoEffect },
            { 6, CardEffect.NoEffect },
            { 7, CardEffect.SpySelf },
            { 8, CardEffect.SpySelf },
            { 9, CardEffect.SpyOther },
            { 10, CardEffect.SpyOther },
            { 11, CardEffect.BlindSwitch },
            { 12, CardEffect.BlindSwitch },
            { 13, CardEffect.SpySwitch },
        };

        public static CardEffect ConvertToCardEffect(int value) {
            return GuardCardValue(value) ? CardEffect.NoEffect : Cards[value];
        }

        public static bool GuardCardValue(int value) {
            return value is < MinCardValue or > MaxCardValue;
        }
    }
}