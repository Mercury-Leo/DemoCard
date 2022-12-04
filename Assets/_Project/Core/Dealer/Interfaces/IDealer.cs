using _Project.Core.Card.Interfaces;

namespace _Project.Core.Dealer.Interfaces{
	public interface IDealer {
		ICard[] Deck { get; set; }
		public ICard[] GenerateDeck();
	}
}