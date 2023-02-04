using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.IDHolders.Scripts;
using _Project.AppUI.SceneLoaders.CardGame.Scripts;
using _Project.Core.Card.Interfaces;
using _Project.Game.Player.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.AppUI.Card.Scripts {
    [RequireComponent(typeof(IDHolder))]
    public class CardViewHandler : MonoBehaviour {
        [Header("Components")] [SerializeField]
        GameObject _playerCards;

        [SerializeField] LayoutGroup _layout;

        [SerializeField] BoundDraggableContainer _container;

        [Header("Loader")] [SerializeField] CardGameLoaderSO _loader;

        ICard[] _cards;

        readonly IList<CardHandler> _cardHandlers = new List<CardHandler>();

        public IDHolder IDHolder => _idHolder;

        IDHolder _idHolder;

        void Awake() {
            TryGetComponent(out _idHolder);
        }

        void OnValidate() {
            if (_playerCards is null)
                Debug.LogError("Player Cards not assigned", this);

            if (_layout is null)
                Debug.LogError("Layout not assigned", this);
        }

        void OnEnable() {
            IDHolder.OnPlayerActive += PlayerActive;
        }

        void OnDisable() {
            IDHolder.OnPlayerActive -= PlayerActive;
        }

        public void SetPlayer(IPlayer player) {
            _idHolder.SetID(player.ID);
            SetPlayerHand(player.GetCards());
        }

        void SetPlayerHand(IEnumerable<ICard> cards) {
            var enumerable = cards as ICard[] ?? cards.ToArray();
            _cards = enumerable;
            for (var i = 0; i < enumerable.Length; i++) {
                var prefab = Instantiate(_loader.Card, _playerCards.transform);
                prefab.SetCardData(_cards[i]);
                prefab.OnCardClicked += CardClicked;
                prefab.OnValueChanged += ValueChanged;
                prefab.SetContainer(_container);
                _cardHandlers.Add(prefab);
            }

            StartCoroutine(DisableLayout());
        }

        void PlayerActive(Guid id, bool state) {
            foreach (var card in _cardHandlers)
                card.IsPlayerActive = state;
        }

        IEnumerator DisableLayout() {
            yield return new WaitForEndOfFrame();
            _layout.enabled = false;
        }

        void CardClicked(ICard card) {
            Debug.LogError($"Clicked card: {card.Value}");
        }
        
        void ValueChanged(int obj) {
            
        }
    }
}