using System.Collections.Generic;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;

namespace _Project.AppUI.Card.Scripts {
    public class CardViewManager : MonoBehaviour {
        [Header("Components")] [SerializeField]
        GameObject _playerCards;

        [Header("Prefabs")] [SerializeField] CardHandler _cardPrefab;

        void OnValidate() {
            if (_playerCards is null)
                this.LogWarning("Player Cards not assigned", this);
        }

        public void SetPlayerHand(IEnumerable<ICard> cards) {
            foreach (var card in cards) {
                var prefab = Instantiate(_cardPrefab);
                prefab.SetCardData(card);
                prefab.OnCardClicked += CardClicked;
            }
        }

        void CardClicked(ICard card) {
            this.Log($"Clicked card: {card.Value}");
        }
    }
}