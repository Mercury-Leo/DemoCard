using System;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.AppUI.Card.Scripts {
    [RequireComponent(typeof(UIDraggableBase))]
    public class CardHandler : UIButton {
        [SerializeField] TMP_Text _cardValue;

        [SerializeField] Image _cover;

        [Header("Card Data")] [ShowInInspector]
        int _cardDataValue;

        [ShowInInspector] Guid _cardID;

        public bool IsPlayerActive {
            get => _isPlayerActive;
            set {
                _isPlayerActive = value;
                _draggedObject.CanBeDragged = value;
            }
        }

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
        public Action OnCardRemoved { get; set; }
        public Action<int> OnValueChanged { get; set; }

        bool _isPlayerActive;
        bool _showValue;
        ICard _card;
        UIDraggableBase _draggedObject;

        protected override void Awake() {
            base.Awake();
            TryGetComponent(out _draggedObject);
        }

        protected override void OnEnable() {
            base.OnEnable();
            _draggedObject.OnObjectEnterHovered += ObjectBeingHovered;
            _draggedObject.OnObjectExitHover += OnObjectExitHover;
            _draggedObject.OnObjectBeingDropped += ObjectDropped;
        }

        protected override void OnDisable() {
            base.OnDisable();
            _draggedObject.OnObjectEnterHovered -= ObjectBeingHovered;
            _draggedObject.OnObjectExitHover -= OnObjectExitHover;
            _draggedObject.OnObjectBeingDropped -= ObjectDropped;
        }

        protected void OnDestroy() {
            _card.OnValueChanged -= ValueChanged;
        }

        void ObjectBeingHovered(Transform hovering) {
            this.LogWarning($"is being hovered {_card.Value}");
            var card = GetCard(hovering);

            this.LogSuccess($"hovering is {card._card.Value}");

            if (card._card.OwnerID.Equals(_card.OwnerID))
                this.LogSuccess("Valid move!");
        }

        void OnObjectExitHover() { }

        /// <summary>
        /// Object Dropped is on the object that a card was dropped on
        /// <example>
        /// If I take card '8' and put it over card '4', this function is run on the '4' card
        /// </example>
        /// </summary>
        /// <param name="droppedObject"></param>
        void ObjectDropped(Transform droppedObject) {
            if (droppedObject == transform)
                return;

            var droppedCard = GetCard(droppedObject);

            if (!droppedCard.IsPlayerActive)
                return;

            if (droppedCard._cardID.Equals(_card.OwnerID))
                return;

            var value = droppedCard._card.Value;
            var myValue = _card.Value;

            _card.Value = value;
            droppedCard._card.Value = myValue;
        }

        public void SetCardData(ICard card) {
            _card = card;
            DisplayCardValue();
            ShowValue = false;
            _card.OnValueChanged += ValueChanged;
            _cardDataValue = _card.Value;
            _cardID = _card.OwnerID;
        }

        public void SetContainer(DraggableContainerBase container) {
            _draggedObject.ContainerBase = container;
        }

        void DisplayCardValue() => CardValue = _card.Value.ToString();

        void ValueChanged() {
            DisplayCardValue();
            OnValueChanged?.Invoke(_card.Value);
        }

        protected override void ButtonClicked() {
            base.ButtonClicked();
            OnCardClicked?.Invoke(_card);
        }

        CardHandler GetCard(Transform trans) {
            if (trans is null)
                return null;

            var card = trans.GetComponent<CardHandler>();

            if (card is null)
                return null;

            return card;
        }
    }
}