using _Project.Core.Card.Interfaces;
using _Project.Core.Dealer.Scripts;
using TMPro;
using UnityEngine;

namespace _Project.AppUI.Card.Scripts {
    public class CardHandler : MonoBehaviour {
        [SerializeField] TMP_Text _cardValue;

        string CardValue {
            set {
                if (_cardValue is null)
                    return;
                _cardValue.text = value;
            }
        }

        void Start() {
            var t = new DeckCreator().Generate();
        }

        ICard _card;

        public void SetCardData(ICard card) {
            _card = card;
            DisplayCardValue();
        }

        void DisplayCardValue() => CardValue = _card.Value.ToString();
    }
}