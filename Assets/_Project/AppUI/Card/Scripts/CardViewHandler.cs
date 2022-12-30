using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.PlayerID.Scripts;
using _Project.AppUI.SceneLoaders.CardGame.Scripts;
using _Project.Core.Card.Interfaces;
using _Project.Game.Player.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.AppUI.Card.Scripts {
    [RequireComponent(typeof(PlayerIDHolder))]
    public class CardViewHandler : MonoBehaviour {
        [Header("Components")] [SerializeField]
        GameObject _playerCards;

        [SerializeField] LayoutGroup _layout;

        [SerializeField] BoundDraggableContainer _container;

        [Header("Loader")] [SerializeField] CardGameLoaderSO _loader;

        ICard[] _cards;

        IList<CardHandler> _cardHandlers;

        PlayerIDHolder _playerID;

        void Awake() {
            TryGetComponent(out _playerID);
        }

        void OnValidate() {
            if (_playerCards is null)
                this.LogWarning("Player Cards not assigned", this);

            if (_layout is null)
                this.LogWarning("Layout not assigned", this);
        }

        void OnEnable() {
            _playerID.OnPlayerActive += PlayerActive;
            _playerID.OnPlayerDeActive += PlayerDeActive;
        }

        void OnDisable() {
            _playerID.OnPlayerActive -= PlayerActive;
            _playerID.OnPlayerDeActive -= PlayerDeActive;
        }

        public void SetPlayer(IPlayer player) {
            _playerID.SetID(player.ID);
            SetPlayerHand(player.GetCards());
        }

        void SetPlayerHand(IEnumerable<ICard> cards) {
            var enumerable = cards as ICard[] ?? cards.ToArray();
            _cards = enumerable;
            for (var i = 0; i < enumerable.Length; i++) {
                var prefab = Instantiate(_loader.Card, _playerCards.transform);
                prefab.SetCardData(_cards[i]);
                prefab.OnCardClicked += CardClicked;
                prefab.SetContainer(_container);
                _cardHandlers.Add(prefab);
            }

            StartCoroutine(DisableLayout());
        }

        void PlayerActive(Guid id) {
            foreach (var card in _cardHandlers)
                card.IsPlayerActive = true;
        }

        void PlayerDeActive(Guid id) {
            foreach (var card in _cardHandlers)
                card.IsPlayerActive = false;
        }


        IEnumerator DisableLayout() {
            yield return new WaitForEndOfFrame();
            _layout.enabled = false;
        }

        void CardClicked(ICard card) {
            this.Log($"Clicked card: {card.Value}");
        }
    }
}