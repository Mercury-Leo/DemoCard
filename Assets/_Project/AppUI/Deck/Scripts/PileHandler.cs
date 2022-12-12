using System.Collections.Generic;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Card.Interfaces;

namespace _Project.AppUI.Deck.Scripts {
    public class PileHandler : UIButton {
        Stack<ICard> _pileCards;
        
        protected override void OnEnable() {
            base.OnEnable();
            OnButtonClicked += DrawCard;
        }
        
        protected override void OnDisable() {
            base.OnDisable();
            OnButtonClicked -= DrawCard;
        }

        void DrawCard() {
            throw new System.NotImplementedException();
        }
        
        public void SetPile(Stack<ICard> cards) {
            _pileCards = cards;
        }
    }
}