namespace _Project.AppUI.Deck.Scripts {
    public class DiscardedPileHandler : CardPileBase {
        protected override void DrawCard() {
            var card = GetCard();

            if (card is null)
                return;
            
            
        }
    }
}