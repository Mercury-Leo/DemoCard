using System.Collections.Generic;
using System.Linq;
using _Project.Core.Card.Interfaces;
using _Project.Core.Card.Scripts;
using _Project.Core.Dealer.Interfaces;
using _Project.Core.Extensions.Collections;

namespace _Project.Core.Dealer.Scripts {
    public class DeckCreator : IDeckCreator {
        public IEnumerable<ICard> Generate(int deckSize, int[] specialValues) {
            var size = deckSize - specialValues.Count();
            var deck = new ICard[deckSize];
            for (var i = 0; i < size; i++)
                deck[i] = new CardData(i % 12 + 1);

            for (var i = size; i < deck.Length; i++)
                deck[i] = new CardData(specialValues[i - size]);

            deck.Shuffle();

            return deck;
        }
    }
}