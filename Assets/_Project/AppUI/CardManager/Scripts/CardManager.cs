using System.Collections.Generic;
using _Project.AppUI.Card.Scripts;
using _Project.AppUI.Deck.Scripts;
using _Project.Game;
using _Project.Game.Player.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;

namespace _Project.AppUI.CardManager.Scripts {
    public class CardManager : MonoBehaviour {
        [SerializeField] GameManager _gameManager;

        [SerializeField] List<CardViewHandler> _cardViewHandlers;

        [SerializeField] DeckManager _deckManager;

        void OnValidate() {
            if (_gameManager is null)
                this.LogWarning("Game Manager isn't assigned!");
        }

        void OnEnable() {
            _gameManager.OnInitializePlayers += InitializePlayers;
        }

        void OnDisable() {
            _gameManager.OnInitializePlayers -= InitializePlayers;
        }

        void InitializePlayers(List<IPlayer> players) {
            for (var i = 0; i < players.Count; i++) {
                if (players[i] is null)
                    return;
                _cardViewHandlers[i].SetPlayerHand(players[i].GetCards());
            }
        }
    }
}