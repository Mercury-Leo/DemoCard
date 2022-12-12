using System;
using System.Collections.Generic;
using _Project.AppUI.Card.Loader;
using _Project.AppUI.Card.Scripts;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.AppUI.Deck.Scripts {
    public class PileHandler : UIButton {
        [SerializeField] string _card;
        [SerializeField] Transform _draggedZone;
        [SerializeField] DraggableContainerBase _container;

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
            AddressableLoader.GetObjectByAddress(_card);
            AddressableLoader.OnGameObjectLoaded += OnGameObjectLoaded;
        }

        void OnGameObjectLoaded(GameObject go) {
            if (!_pileCards.TryPop(out var cardResult)) {
                OnPileEmpty?.Invoke();
                this.Log("Failed to pop card");
                return;
            }
            
            var card = go.GetComponent<CardHandler>();
            if (card is null)
                return;

            var cardHandler = Instantiate(card, _draggedZone);
            cardHandler.SetContainer(_container);

            cardHandler.SetCardData(cardResult);
            cardHandler.ShowValue = true;
            OnCardDrew?.Invoke();
            AddressableLoader.OnGameObjectLoaded -= OnGameObjectLoaded;
        }

        public void SetPile(Stack<ICard> cards) {
            _pileCards = cards;
        }
    }
}