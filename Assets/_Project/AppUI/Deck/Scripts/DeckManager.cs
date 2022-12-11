using _Project.AppUI.Components.Draggable.Scripts;
using _Project.Core.Dealer.Interfaces;
using UnityEngine;

namespace _Project.AppUI.Deck.Scripts {
    [RequireComponent(typeof(DraggableContainerBase))]
    public class DeckManager : MonoBehaviour {

        [SerializeField] PileHandler _pile;

        [SerializeField] DiscardedPileHandler _discarded;

        public void SetDeck(IDeck deck) {
            
        }
    }
}