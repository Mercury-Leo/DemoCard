using System.Collections.Generic;
using _Project.AppUI.Card.Scripts;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;

namespace _Project.AppUI.Deck.Scripts {
    public class PileHandler : UIButton {
        [SerializeField] CardHandler _card;
        [SerializeField] Transform _draggedZone;

        [SerializeField] DraggableContainerBase _container;
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
            var card = Instantiate(_card, _draggedZone);
            card.SetContainer(_container);
            if (!_pileCards.TryPop(out var cardResult)) {
                this.LogWarning("Failed to pop card");
                return;
            }

            card.SetCardData(cardResult);
            card.ShowValue = true;
        }

        public void SetPile(Stack<ICard> cards) {
            _pileCards = cards;
        }
    }
}