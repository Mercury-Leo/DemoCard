using System.Collections.Generic;
using _Project.Core.Enums.CardEffects;
using static _Project.Core.Enums.CardEffects.CardEffects;
using static _Project.Core.CoreConventions;

namespace _Project.Core.Extensions.Card {
    public static class CardExtensions {
        static readonly Dictionary<int, CardEffects> Cards = new Dictionary<int, CardEffects>() {
            { -1, NoEffect },
            { 0, NoEffect },
            { 1, NoEffect },
            { 2, NoEffect },
            { 3, NoEffect },
            { 4, NoEffect },
            { 5, NoEffect },
            { 6, NoEffect },
            { 7, SpySelf },
            { 8, SpySelf },
            { 9, SpyOther },
            { 10, SpyOther },
            { 11, BlindSwitch },
            { 12, BlindSwitch },
            { 13, SpySwitch },
        };

        public static CardEffects ConvertToCardEffect(int value) {
            return GuardCardValue(value) ? NoEffect : Cards[value];
        }

        public static bool GuardCardValue(int value) {
            return value is < MinCardValue or > MaxCardValue;
        }
    }
}