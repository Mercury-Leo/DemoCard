using _Project.AppUI.Components.Draggable.Scripts;
using _Project.Core.Dealer.Interfaces;
using _Project.Game;
using UnityEngine;

namespace _Project.AppUI.Deck.Scripts {
    [RequireComponent(typeof(DraggableContainerBase))]
    public class DeckManager : MonoBehaviour {
        [SerializeField] PileHandler _pile;

        [SerializeField] DiscardedPileHandler _discarded;

        [SerializeField] GameManager _gameManager;

        void OnEnable() {
            _gameManager.OnDeckDealt += OnDeckDealt;
        }

        void OnDisable() {
            _gameManager.OnDeckDealt -= OnDeckDealt;
        }

        void OnDeckDealt(IDeck deck) {
            SetDeck(deck);
        }

        public void SetDeck(IDeck deck) {
            _pile.SetPile(deck.CardPile);
        }
    }
}