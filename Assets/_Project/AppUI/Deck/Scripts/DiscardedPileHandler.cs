namespace _Project.AppUI.Deck.Scripts {
    public class DiscardedPileHandler : CardPileBase {
        protected override void DrawCard() {
            var card = GetCardHandler();

            if (card is null)
                return;

            card.IsDraggable = true;
        }
    }
}