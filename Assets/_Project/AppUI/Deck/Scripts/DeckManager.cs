using _Project.Core.Dealer.Interfaces;
using UnityEngine;

namespace _Project.AppUI.Deck.Scripts {
    public class DeckManager : MonoBehaviour {

        [SerializeField] PileHandler _pile;

        [SerializeField] DiscardedPileHandler _discarded;

        public void SetDeck(IDeck deck) {
            
        }
    }
}