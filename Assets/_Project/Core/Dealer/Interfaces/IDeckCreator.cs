using System.Collections.Generic;
using _Project.Core.Card.Interfaces;

namespace _Project.Core.Dealer.Interfaces{
	public interface IDeckCreator {
		public IEnumerable<ICard> Generate(int deckSize, int[] specialValues);
	}
}