using System;
using System.Collections.Generic;
using _Project.Core.Card.Interfaces;
using _Project.Game.Player.Interfaces;
using Editor.Logger.Scripts;

namespace _Project.Game.Player.Scripts {
    public class CardPlayer : IPlayer {
        public Guid PlayerID { get; set; }
        public List<ICard> Cards { get; set; }

        public void PopulateCards(IList<ICard> cards) {
            foreach (var card in cards)
                Cards.Add(card);
        }

        public ICard PerformAction(ICard card) {
            return card;
        }

        public ICard SwapCards(ICard cardToRemove, ICard cardToInsert) {
            var index = Cards.IndexOf(cardToRemove);
            var cardRemoved = SwapCards(index, cardToInsert);
            return cardRemoved;
        }

        public ICard SwapCards(int cardPosition, ICard cardToInsert) {
            var cardRemoved = Cards[cardPosition];
            Cards[cardPosition] = cardToInsert;
            return cardRemoved;
        }

        public ICard RemoveCard(ICard cardToRemove) {
            var didRemove = Cards.Remove(cardToRemove);
            if (didRemove)
                return cardToRemove;
            this.LogError($"Failed to remove Card: {cardToRemove.Value} from player: {PlayerID}");
            return null;
        }

        public ICard RemoveCard(int cardPosition) {
            var cardRemoved = Cards[cardPosition];
            Cards.RemoveAt(cardPosition);
            return cardRemoved;
        }
    }
}