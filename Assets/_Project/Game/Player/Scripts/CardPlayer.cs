using System;
using System.Collections.Generic;
using _Project.Core.Card.Interfaces;
using _Project.Game.Player.Interfaces;
using UnityEngine;

namespace _Project.Game.Player.Scripts {
    public class CardPlayer : IPlayer {
        public Guid ID { get; set; }

        readonly List<ICard> _cards = new();

        public void PopulateCards(IEnumerable<ICard> cards) {
            foreach (var card in cards) {
                card.OwnerID = ID;
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
            Debug.LogError($"Failed to remove Card: {cardToRemove.Value} from player: {ID}");
            return null;
        }

        public ICard RemoveCard(int cardPosition) {
            var cardRemoved = _cards[cardPosition];
            _cards.RemoveAt(cardPosition);
            return cardRemoved;
        }
    }
}