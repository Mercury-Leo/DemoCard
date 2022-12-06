using System;
using System.Collections.Generic;
using _Project.Core.Card.Interfaces;

namespace _Project.Game.Player.Interfaces {
    public interface IPlayer {
        public abstract Guid PlayerID { get; set; }

        public abstract IList<ICard> Cards { get; set; }

        public abstract ICard PerformAction(ICard card);

        public abstract void SwapCards(ICard cardToRemove, ICard cardToInsert);

        public abstract void SwapCards(int cardPosition, ICard cardToInsert);
    }
}