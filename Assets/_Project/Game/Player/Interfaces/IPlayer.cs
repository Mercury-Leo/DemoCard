using System;
using System.Collections.Generic;
using _Project.Core.Card.Interfaces;

namespace _Project.Game.Player.Interfaces {
    public interface IPlayer {
        public Guid PlayerID { get; set; }

        public IList<ICard> Cards { get; set; }
    }
}