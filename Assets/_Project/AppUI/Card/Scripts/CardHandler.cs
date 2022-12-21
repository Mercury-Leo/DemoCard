using System;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.AppUI.Card.Scripts {
    [RequireComponent(typeof(UIDraggableBase))]
    public class CardHandler : UIButton {
        [SerializeField] TMP_Text _cardValue;

        [SerializeField] Image _cover;

        string CardValue {
            set {
                if (_cardValue is null)
                    return;
                _cardValue.text = value;
            }
        }

        public bool ShowValue {
            get => _showValue;
            set {
                _showValue = value;
                _cover.gameObject.SetActive(!value);
            }
        }

        public Action<ICard> OnCardClicked { get; set; }

        ICard _card;
        bool _showValue;
        UIDraggableBase _draggedObject;

        protected override void Awake() {
            base.Awake();
            TryGetComponent(out _draggedObject);
        }

        protected override void OnEnable() {
            base.OnEnable();
            _draggedObject.OnObjectEnterHovered += OnObjectBeingHovered;
            _draggedObject.OnObjectExitHover += OnObjectExitHover;
        }

        protected override void OnDisable() {
            base.OnDisable();
            _draggedObject.OnObjectEnterHovered -= OnObjectBeingHovered;
            _draggedObject.OnObjectExitHover -= OnObjectExitHover;
        }

        void OnObjectBeingHovered(Transform hovering) {
            this.LogWarning($"is being hovered {_card.Value}");
            var card = hovering.GetComponent<CardHandler>();
            if (card is null)
                return;

            this.LogSuccess($"hovering is {card._card.Value}");
            
            if(card._card.OwnerID.Equals(_card.OwnerID))
                this.LogSuccess("Valid move!");
        }

        void OnObjectExitHover() {
            
        }

        public void SetCardData(ICard card) {
            _card = card;
            DisplayCardValue();
            ShowValue = false;
        }

        public void SetContainer(DraggableContainerBase container) {
            _draggedObject.ContainerBase = container;
        }

        void DisplayCardValue() => CardValue = _card.Value.ToString();

        protected override void ButtonClicked() {
            base.ButtonClicked();
            OnCardClicked?.Invoke(_card);
        }
    }
}