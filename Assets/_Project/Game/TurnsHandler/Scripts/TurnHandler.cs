using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Core.TurnManager.Interfaces;
using _Project.Core.TurnManager.Scripts;
using _Project.Game.Player.Interfaces;
using UnityEngine;
using static _Project.Game.GameConventions;

namespace _Project.Game.TurnsHandler.Scripts {
    public class TurnHandler : MonoBehaviour {
        [SerializeField] TurnManager _turnManager;

        readonly Dictionary<IPlayer, ITurn> _playerTurns = new();
        int _turnIndex;

        public Action<Guid> OnPlayerTurn { get; set; }

        void OnEnable() {
            _turnManager.OnTurnLose += EndTurn;
        }

        void OnDisable() {
            _turnManager.OnTurnLose -= EndTurn;
        }

        public void EndTurn() {
            _turnManager.NextTurn(GetNextTurn());
        }

        public void AddPlayer(IPlayer player) {
            if (player is null)
                return;

            CreatePlayerTurn(player);
        }

        public void AddPlayers(IEnumerable<IPlayer> players) {
            if (players is null)
                return;

            foreach (var player in players)
                CreatePlayerTurn(player);
        }

        public void RemovePlayer(IPlayer player) {
            if (player is null)
                return;

            _playerTurns[player] = null;
        }

        public void StartTurns() {
            if (_playerTurns.Count <= 0)
                return;

            _turnManager.StartTurn(GetNextTurn());
        }

        void CreatePlayerTurn(IPlayer player) {
            if (_playerTurns.ContainsKey(player)) {
                _playerTurns[player] = new TurnPlaceholder(TurnTime, player.ID);
                return;
            }

            _playerTurns.Add(player, new TurnPlaceholder(TurnTime, player.ID));
        }

        ITurn GetNextTurn() {
            var turn = _playerTurns.Values.ElementAt(_turnIndex);

            // If a user is not online, try move to next player
            while (turn is null) {
                TryIncrementTurnIndex();
                if (_turnIndex >= _playerTurns.Count)
                    return null;
                turn = _playerTurns.Values.ElementAt(_turnIndex);
            }

            OnPlayerTurn?.Invoke(turn.UserID);

            TryIncrementTurnIndex();

            return turn;
        }

        void TryIncrementTurnIndex() {
            _turnIndex++;

            if (_turnIndex >= _playerTurns.Count)
                _turnIndex = 0;
        }
    }
}