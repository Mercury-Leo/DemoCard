namespace _Project.AppUI.Deck.Scripts {
    public class PileHandler : CardPileBase {
        protected override void DrawCard() {
            var card = GetCard();

            if (card is null)
                return;

            var handler = CreateCard(card);
            
            handler.IsDraggable = true;
        }

        protected override void DrawStateChanged(bool state) { }
    }
}