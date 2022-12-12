using _Project.Core.Card.Interfaces;

namespace _Project.Core.Placeholders {
    public class CardSwapPlaceholder {
        ICard SentCard { get; set; }
        ICard ReceivedCard { get; set; }

        public CardSwapPlaceholder(ICard sentCard, ICard receivedCard) {
            SentCard = sentCard;
            ReceivedCard = receivedCard;
        }
    }
}