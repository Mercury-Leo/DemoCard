using System;
using System.Collections.Generic;
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
        [SerializeField] AssetReference _card;
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
            var operation = _card.LoadAssetAsync<CardHandler>();
            operation.Completed += handle => {
                if (handle.Status is not AsyncOperationStatus.Succeeded) {
                    this.LogError("Failed to load card.", this);
                    return;
                }

                var card = Instantiate(handle.Result, _draggedZone);
                card.SetContainer(_container);
                if (!_pileCards.TryPop(out var cardResult)) {
                    OnPileEmpty?.Invoke();
                    this.Log("Failed to pop card");
                    return;
                }

                card.SetCardData(cardResult);
                card.ShowValue = true;
                OnCardDrew?.Invoke();
            };
        }

        public void SetPile(Stack<ICard> cards) {
            _pileCards = cards;
        }
    }
}