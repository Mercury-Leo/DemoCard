using System.Collections.Generic;
using System.Linq;
using _Project.AppUI.Card.Loader;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.AppUI.Card.Scripts {
    [RequireComponent(typeof(DraggableContainerBase))]
    public class CardViewHandler : MonoBehaviour {
        [Header("Components")] [SerializeField]
        GameObject _playerCards;

        [Header("Prefabs")] [SerializeField] AssetReference _cardPrefab;
        
        DraggableContainerBase _container;

        ICard[] _cards;
        int index = 0;

        protected void Awake() {
            TryGetComponent(out _container);
        }

        void OnValidate() {
            if (_playerCards is null)
                this.LogWarning("Player Cards not assigned", this);
        }

        public void SetPlayerHand(IEnumerable<ICard> cards) {
            var enumerable = cards as ICard[] ?? cards.ToArray();
            _cards = enumerable;
            for (var i = 0; i < enumerable.Length; i++) {
                AddressableLoader.OnGameObjectLoaded += CardCreated;
                AddressableLoader.GetObjectByReference(_cardPrefab);
                index++;
            }
        }

        void CardCreated(GameObject obj) {
            var card = obj.GetComponent<CardHandler>();
            if (card is null)
                return;
            var prefab = Instantiate(card, _playerCards.transform);
            prefab.SetCardData(_cards[index]);
            prefab.OnCardClicked += CardClicked;
            prefab.SetContainer(_container);
            AddressableLoader.OnGameObjectLoaded -= CardCreated;
        }

        void CardClicked(ICard card) {
            this.Log($"Clicked card: {card.Value}");
        }
    }
}