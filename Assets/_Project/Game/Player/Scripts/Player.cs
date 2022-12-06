using System;
using System.Collections.Generic;
using _Project.Core.Card.Interfaces;
using _Project.Game.Player.Interfaces;

namespace _Project.Game.Player.Scripts{
	public class Player : IPlayer{
		public Guid PlayerID { get; set; }
		public IList<ICard> Cards { get; set; }
		public ICard PerformAction(ICard card) {
			throw new NotImplementedException();
		}

		public void SwapCards(ICard cardToRemove, ICard cardToInsert) {
			throw new NotImplementedException();
		}

		public void SwapCards(int cardPosition, ICard cardToInsert) {
			throw new NotImplementedException();
		}
	}
}