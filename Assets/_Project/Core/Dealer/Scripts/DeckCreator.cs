using System.Collections.Generic;
using _Project.Core.Card.Interfaces;
using _Project.Core.Card.Scripts;
using _Project.Core.Dealer.Interfaces;
using _Project.Core.Extensions.Collections;
using static _Project.Core.CoreConventions;

namespace _Project.Core.Dealer.Scripts {
    public class DeckCreator : IDeckCreator {
        public IEnumerable<ICard> Generate() {
            const int size = DeckSize - SpecialCards;
            var deck = new ICard[DeckSize];
            for (var i = 0; i < size; i++)
                deck[i] = new CardData(i % 12 + 1);

            for (var i = size; i < deck.Length; i++)
                deck[i] = new CardData(SpecialValues[i - size]);

            deck.Shuffle();

            return deck;
        }
    }
}