using System;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Card.Interfaces;
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
        UIDraggableBase _draggableObject;

        protected override void Awake() {
            base.Awake();
            TryGetComponent(out _draggableObject);
        }

        public void SetCardData(ICard card) {
            _card = card;
            DisplayCardValue();
            ShowValue = false;
        }

        public void SetContainer(DraggableContainerBase container) {
            _draggableObject.ContainerBase = container;
        }

        void DisplayCardValue() => CardValue = _card.Value.ToString();

        protected override void ButtonClicked() {
            base.ButtonClicked();
            OnCardClicked?.Invoke(_card);
        }
    }
}