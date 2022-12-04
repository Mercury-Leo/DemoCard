using _Project.Core.Card.Interfaces;
using _Project.Core.Card.Scripts;
using _Project.Core.Dealer.Interfaces;
using _Project.Core.Extensions.Collections;
using static _Project.Core.CoreConventions;

namespace _Project.Core.Dealer.Scripts {
    public class Dealer : IDealer {
        public ICard[] Deck { get; set; } = new ICard[DeckSize];

        public ICard[] GenerateDeck() {
            const int size = DeckSize - SpecialCards;
            for (var i = 0; i < size; i++)
                Deck[i] = new CardData(i % 12 + 1);

            for (var i = size; i < Deck.Length; i++)
                Deck[i] = new CardData(specialValues[i - size]);

            Deck.Shuffle();

            return Deck;
        }
    }
}