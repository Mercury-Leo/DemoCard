using System;
using System.Collections.Generic;
using _Project.Core.Card.Interfaces;
using _Project.Game.Player.Interfaces;
using Editor.Logger.Scripts;

namespace _Project.Game.Player.Scripts {
    public class CardPlayer : IPlayer {
        public Guid PlayerID { get; set; }

        readonly List<ICard> _cards = new();

        public void PopulateCards(IEnumerable<ICard> cards) {
            foreach (var card in cards) {
                card.OwnerID = PlayerID;
                _cards.Add(card);
            }
        }

        public IEnumerable<ICard> GetCards() {
            return _cards;
        }

        public ICard PerformAction(ICard card) {
            return card;
        }

        public ICard SwapCards(ICard cardToRemove, ICard cardToInsert) {
            var index = _cards.IndexOf(cardToRemove);
            var cardRemoved = SwapCards(index, cardToInsert);
            return cardRemoved;
        }

        public ICard SwapCards(int cardPosition, ICard cardToInsert) {
            var cardRemoved = _cards[cardPosition];
            _cards[cardPosition] = cardToInsert;
            return cardRemoved;
        }

        public ICard RemoveCard(ICard cardToRemove) {
            var didRemove = _cards.Remove(cardToRemove);
            if (didRemove)
                return cardToRemove;
            this.LogError($"Failed to remove Card: {cardToRemove.Value} from player: {PlayerID}");
            return null;
        }

        public ICard RemoveCard(int cardPosition) {
            var cardRemoved = _cards[cardPosition];
            _cards.RemoveAt(cardPosition);
            return cardRemoved;
        }
    }
}