using System;
using System.Collections.Generic;
using _Project.AppUI.Card.Scripts;
using _Project.AppUI.Deck.Scripts;
using _Project.AppUI.IDHolders.Scripts;
using _Project.Game;
using _Project.Game.Player.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;

namespace _Project.AppUI.CardManager.Scripts {
    public class CardManager : MonoBehaviour {
        [SerializeField] GameManager _gameManager;

        [SerializeField] List<CardViewHandler> _cardViewHandlers;

        [SerializeField] DeckManager _deckManager;

        readonly IList<IDHolder> _idHolders = new List<IDHolder>();

        void OnValidate() {
            if (_gameManager is null)
                this.LogWarning("Game Manager isn't assigned!");
        }

        void OnEnable() {
            _gameManager.OnInitializePlayers += InitializePlayers;
            _gameManager.OnActivePlayer += ActivePlayer;
        }

        void OnDisable() {
            _gameManager.OnInitializePlayers -= InitializePlayers;
            _gameManager.OnActivePlayer -= ActivePlayer;
        }

        void InitializePlayers(List<IPlayer> players) {
            for (var i = 0; i < players.Count; i++) {
                if (players[i] is null)
                    return;
                _cardViewHandlers[i].SetPlayer(players[i]);
                _idHolders.Add(_cardViewHandlers[i].IDHolder);
            }
        }

        void ActivePlayer(Guid id) {
            foreach (var playerID in _idHolders)
                playerID.IsActivePlayer = playerID.PlayerID.Equals(id);
        }
    }
}