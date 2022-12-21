using System;
using System.Collections.Generic;
using _Project.Core.Card.Interfaces;

namespace _Project.Game.Player.Interfaces {
    public interface IPlayer {
        public Guid PlayerID { get; set; }
        public void PopulateCards(IEnumerable<ICard> cards);
        public IEnumerable<ICard> GetCards();
        public ICard PerformAction(ICard card);
        public ICard SwapCards(ICard cardToRemove, ICard cardToInsert);
        public ICard SwapCards(int cardPosition, ICard cardToInsert);
        public ICard RemoveCard(ICard cardToRemove);
        public ICard RemoveCard(int cardPosition);
    }
}