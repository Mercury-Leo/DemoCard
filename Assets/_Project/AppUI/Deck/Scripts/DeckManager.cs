using System;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.Core.Dealer.Interfaces;
using _Project.Game;
using UnityEngine;

namespace _Project.AppUI.Deck.Scripts {
    [RequireComponent(typeof(DraggableContainerBase))]
    public class DeckManager : MonoBehaviour {
        [SerializeField] CardPileBase _pile;

        [SerializeField] CardPileBase _discarded;

        [SerializeField] GameManager _gameManager;
        
        public Action OnCardDrew { get; set; }

        void OnEnable() {
            _gameManager.OnDeckDealt += OnDeckDealt;
            _pile.OnCardDrew += CardDrew;
            _discarded.OnCardDrew += CardDrew;
        }

        void OnDisable() {
            _gameManager.OnDeckDealt -= OnDeckDealt;
            _pile.OnCardDrew -= CardDrew;
            _discarded.OnCardDrew -= CardDrew;
        }

        void OnDeckDealt(IDeck deck) {
            SetDeck(deck);
        }

        public void SetDeck(IDeck deck) {
            _pile.SetPile(deck.CardPile);
        }
        
        void CardDrew() {
           OnCardDrew?.Invoke();
        }
    }
}