using System;
using System.Collections.Generic;
using _Project.AppUI.Components.Scripts;
using _Project.Core.Placeholders;
using _Project.Game;
using _Project.Game.Player.Interfaces;
using UnityEngine;

namespace _Project.AppUI.PlayerTurnBroadcaster {
    public class TurnBroadcaster : MonoBehaviour {
        [SerializeField] UIText _text;

        [SerializeField] GameManager _gameManager;

        readonly Dictionary<Guid, TextPlaceholder> _turnTexts = new();

        const string TurnSuffix = "'s Turn";

        void OnEnable() {
            _gameManager.OnInitializePlayers += SetPlayers;
            _gameManager.OnActivePlayerChanged += OnActivePlayer;
        }

        void OnDisable() {
            _gameManager.OnInitializePlayers -= SetPlayers;
            _gameManager.OnActivePlayerChanged -= OnActivePlayer;
        }

        void SetPlayers(IList<IPlayer> players) {
            foreach (var player in players)
                AddTurnText(player.ID);
        }

        void OnActivePlayer(Guid id) {
            if (!_turnTexts.ContainsKey(id)) {
                AddTurnText(id);
            }

            _text.SetText(_turnTexts[id]);
        }

        void AddTurnText(Guid id) {
            var turnText = new TextPlaceholder(id.ToString(), string.Empty, TurnSuffix);
            _turnTexts.Add(id, turnText);
        }
    }
}