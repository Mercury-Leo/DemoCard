using System;
using System.Collections.Generic;
using _Project.Core.Card.Interfaces;

namespace _Project.Game.Player.Interfaces {
    public interface IPlayer {
        public abstract Guid PlayerID { get; set; }
        public abstract List<ICard> Cards { get; set; }
        public abstract ICard PerformAction(ICard card);
        public abstract ICard SwapCards(ICard cardToRemove, ICard cardToInsert);
        public abstract ICard SwapCards(int cardPosition, ICard cardToInsert);
        public abstract ICard RemoveCard(ICard cardToRemove);
        public abstract ICard RemoveCard(int cardPosition);
    }
}