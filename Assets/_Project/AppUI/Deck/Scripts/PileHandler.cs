using System;
using System.Collections.Generic;
using _Project.AppUI.Card.Scripts;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.Components.Scripts;
using _Project.AppUI.SceneLoaders.CardGame.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;

namespace _Project.AppUI.Deck.Scripts {
    public class PileHandler : UIButton {
        [SerializeField] Transform _draggedZone;
        [SerializeField] DraggableContainerBase _container;

        [Header("Loader")] [SerializeField] CardGameLoaderSO _loader;

        public Action OnCardDrew { get; set; }
        public Action OnPileEmpty { get; set; }

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
            OnGameObjectLoaded(_loader.Card);
        }

        void OnGameObjectLoaded(CardHandler card) {
            if (!_pileCards.TryPop(out var cardResult)) {
                OnPileEmpty?.Invoke();
                this.Log("Failed to pop card");
                return;
            }

            var cardHandler = Instantiate(card, _draggedZone);
            cardHandler.SetContainer(_container);

            cardHandler.SetCardData(cardResult);
            cardHandler.ShowValue = true;
            OnCardDrew?.Invoke();
        }

        public void SetPile(Stack<ICard> cards) {
            _pileCards = cards;
        }
    }
}