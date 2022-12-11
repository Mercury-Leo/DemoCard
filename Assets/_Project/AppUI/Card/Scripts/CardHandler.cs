using System;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Card.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.AppUI.Card.Scripts {
    public class CardHandler : UIButtonDraggable {
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

        bool _showValue;

        public Action<ICard> OnCardClicked { get; set; }

        ICard _card;

        public void SetCardData(ICard card) {
            _card = card;
            DisplayCardValue();
            ShowValue = false;
        }

        void DisplayCardValue() => CardValue = _card.Value.ToString();

        protected override void ButtonClicked() {
            base.ButtonClicked();
            OnCardClicked?.Invoke(_card);
        }
    }
}