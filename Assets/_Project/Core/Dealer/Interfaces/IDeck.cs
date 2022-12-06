using System.Collections.Generic;
using _Project.Core.Card.Interfaces;

namespace _Project.Core.Dealer.Interfaces {
	public interface IDeck {
		public Stack<ICard> CardPile { get; set; }
		
		public Stack<ICard> DiscardedCards { get; set; }

		public ICard Draw();

		public ICard[] Draw(int amount);

		public void ReshuffleCards();
	}
}