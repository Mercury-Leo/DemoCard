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

        public bool ShowValue {
            get => _showValue;
            set {
                _showValue = value;
                _cover.gameObject.SetActive(!value);
            }
        }

        public bool IsDraggable {
            set {
                if (_draggedObject is null)
                    return;
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

        public ICard Data { get; private set; }

        public Action<ICard> OnCardClicked { get; set; }
        public Action OnCardRemoved { get; set; }
        public Action<int> OnValueChanged { get; set; }

        bool _isPlayerActive;
        bool _showValue;
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
            Data.OnValueChanged -= ValueChanged;
        }

        void ObjectBeingHovered(Transform hovering) {
            this.LogWarning($"is being hovered {Data.Value}");
            var card = GetCard(hovering);

            this.LogSuccess($"hovering is {card.Data.Value}");

            if (card.Data.OwnerID.Equals(Data.OwnerID))
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

            if (droppedCard._cardID.Equals(Data.OwnerID))
                return;

            var value = droppedCard.Data.Value;
            var myValue = Data.Value;

            Data.Value = value;
            droppedCard.Data.Value = myValue;
        }

        public void SetCardData(ICard card) {
            Data = card;
            DisplayCardValue();
            ShowValue = false;
            Data.OnValueChanged += ValueChanged;
            _cardDataValue = Data.Value;
            _cardID = Data.OwnerID;
        }

        public void SetContainer(DraggableContainerBase container) {
            _draggedObject.ContainerBase = container;
        }

        void DisplayCardValue() => CardValue = Data.Value.ToString();

        void ValueChanged() {
            DisplayCardValue();
            OnValueChanged?.Invoke(Data.Value);
        }

        protected override void ButtonClicked() {
            base.ButtonClicked();
            OnCardClicked?.Invoke(Data);
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