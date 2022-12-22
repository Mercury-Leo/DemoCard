using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.SceneLoaders.CardGame.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.AppUI.Card.Scripts {
    public class CardViewHandler : MonoBehaviour {
        [Header("Components")] [SerializeField]
        GameObject _playerCards;

        [SerializeField] LayoutGroup _layout;

        [SerializeField] BoundDraggableContainer _container;

        [Header("Prefabs")] [SerializeField] CardGameLoaderSO _loader;

        ICard[] _cards;

        void OnValidate() {
            if (_playerCards is null)
                this.LogWarning("Player Cards not assigned", this);

            if (_layout is null)
                this.LogWarning("Layout not assigned", this);
        }

        public void SetPlayerHand(IEnumerable<ICard> cards) {
            var enumerable = cards as ICard[] ?? cards.ToArray();
            _cards = enumerable;
            for (var i = 0; i < enumerable.Length; i++) {
                var prefab = Instantiate(_loader.Card, _playerCards.transform);
                prefab.SetCardData(_cards[i]);
                prefab.OnCardClicked += CardClicked;
                prefab.SetContainer(_container);
            }

            StartCoroutine(DisableLayout());
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