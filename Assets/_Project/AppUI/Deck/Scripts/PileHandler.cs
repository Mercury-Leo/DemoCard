namespace _Project.AppUI.Deck.Scripts {
    public class PileHandler : CardPileBase {
        protected override void DrawCard() {
            var card = GetCard();

            if (card is null)
                return;

            CreateCard(card);
        }
    }
}