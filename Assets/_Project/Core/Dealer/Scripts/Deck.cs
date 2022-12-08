using System.Collections.Generic;
using _Project.Core.Card.Interfaces;
using _Project.Core.Dealer.Interfaces;
using _Project.Core.Extensions.Collections;
using Editor.Logger.Scripts;

namespace _Project.Core.Dealer.Scripts {
    public class Deck : IDeck {
        public Stack<ICard> CardPile { get; set; }
        public Stack<ICard> DiscardedCards { get; set; }

        public Deck(IDeckCreator dealer) {
            CardPile = new Stack<ICard>(dealer.Generate());
            DiscardedCards = new Stack<ICard>();
        }

        public ICard Draw() {
            var draw = CardPile.TryPop(out var card);

            if (draw)
                return card;
            this.LogError("Failed to draw card!");
            return null;
        }

        public IEnumerable<ICard> Draw(int amount) {
            if (amount <= 0)
                return null;
            var cards = new ICard[amount];
            for (var i = 0; i < amount; i++) {
                cards[i] = Draw();
                if (CardPile.Count <= 0)
                    ReshuffleCards();
            }

            return cards;
        }

        public void ReshuffleCards() {
            var discardedCards = DiscardedCards.ToArray();
            var pile = CardPile.ToArray();
            var cards = pile.MergeArrays(discardedCards);
            cards.Shuffle();
            DiscardedCards.Clear();
            CardPile = new Stack<ICard>(cards);
        }
    }
}