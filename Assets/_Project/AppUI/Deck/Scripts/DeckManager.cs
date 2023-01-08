using System;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.Core;
using _Project.Core.Card.Interfaces;
using _Project.Core.Dealer.Interfaces;
using _Project.Core.TurnManager.Scripts;
using _Project.Game;
using _Project.Game.TurnEffect;
using UnityEngine;

namespace _Project.AppUI.Deck.Scripts {
    [RequireComponent(typeof(DraggableContainerBase))]
    public class DeckManager : MonoBehaviour {
        [SerializeField] CardPileBase _pile;

        [SerializeField] CardPileBase _discarded;

        public Action OnCardDrew { get; set; }
        
        bool CanDrawCard {
            set {
                _pile.CanDraw = value;
                _discarded.CanDraw = value;
            }
        }

        void OnEnable() {
            _pile.OnCardDrew += DrewCard;
            _discarded.OnCardDrew += DrewCard;

            GameManager.Instance.OnDeckDealt += DeckDealt;

            TurnManager.Instance.OnTurnBegin += TurnBegin;
            TurnManager.Instance.OnTurnEnded += TurnEnded;
        }

        void OnDisable() {
            _pile.OnCardDrew -= DrewCard;
            _discarded.OnCardDrew -= DrewCard;

            GameManager.Instance.OnDeckDealt -= DeckDealt;

            TurnManager.Instance.OnTurnBegin -= TurnBegin;
            TurnManager.Instance.OnTurnEnded -= TurnEnded;
        }

        public void SetDeck(IDeck deck) {
            _pile.SetPile(deck.CardPile);
        }

        void DeckDealt(IDeck deck) {
            SetDeck(deck);
        }

        void TurnBegin() {
            CanDrawCard = true;
        }

        void TurnEnded() {
            CanDrawCard = false;
        }

        void DrewCard(ICard card) {
            OnCardDrew?.Invoke();
            CanDrawCard = false;
            if (card.CardEffect is CoreConventions.CardEffect.NoEffect)
                return;

            PlayerEffectManager.Instance.SetCurrentTurnEffect(card.CardEffect);
        }
    }
}