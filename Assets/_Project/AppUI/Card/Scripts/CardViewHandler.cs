using System.Collections.Generic;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;

namespace _Project.AppUI.Card.Scripts {
    [RequireComponent(typeof(BoundDraggableContainer))]
    public class CardViewHandler : MonoBehaviour {
        [Header("Components")] [SerializeField]
        GameObject _playerCards;

        [Header("Prefabs")] [SerializeField] CardHandler _cardPrefab;
        
        DraggableContainerBase _container;

        protected void Awake() {
            TryGetComponent(out _container);
        }

        void OnValidate() {
            if (_playerCards is null)
                this.LogWarning("Player Cards not assigned", this);
        }

        public void SetPlayerHand(IEnumerable<ICard> cards) {
            foreach (var card in cards) {
                var prefab = Instantiate(_cardPrefab, _playerCards.transform);
                prefab.SetCardData(card);
                prefab.OnCardClicked += CardClicked;
                prefab.SetContainer(_container);
            }
        }

        void CardClicked(ICard card) {
            this.Log($"Clicked card: {card.Value}");
        }
    }
}