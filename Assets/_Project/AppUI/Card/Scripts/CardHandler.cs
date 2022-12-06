using System;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Card.Interfaces;
using TMPro;
using UnityEngine;

namespace _Project.AppUI.Card.Scripts {
    public class CardHandler : UIButton {
        [SerializeField] TMP_Text _cardValue;

        string CardValue {
            set {
                if (_cardValue is null)
                    return;
                _cardValue.text = value;
            }
        }

        public Action<ICard> OnCardClicked { get; set; }

        ICard _card;

        public void SetCardData(ICard card) {
            _card = card;
            DisplayCardValue();
        }

        void DisplayCardValue() => CardValue = _card.Value.ToString();

        protected override void ButtonClicked() {
            base.ButtonClicked();
            OnCardClicked?.Invoke(_card);
        }
    }
}