using _Project.Core.Card.Interfaces;

namespace _Project.AppUI.Deck.Scripts {
    public class DiscardedPileHandler : CardPileBase {
        protected override void DrawCard() {
            var card = GetCardHandler();

            if (card is null)
                return;

            card.IsDraggable = true;
        }

        protected override void DrawStateChanged(bool state) {
            if (PileCardHandlers.TryPeek(out var card)) {
                card.IsDraggable = state;
                card.OnCardClicked += OnCardClicked;
            }
        }

        void OnCardClicked(ICard data) {
            OnCardDrew?.Invoke(CreateCard(data));
        }
    }
}