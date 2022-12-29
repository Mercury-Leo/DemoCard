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

        void OnEnable() {
            
        }

        void OnDisable() {
            
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
            _playerTurns.Add(player, new TurnPlaceholder(TurnTime, player.PlayerID));
        }

        ITurn GetNextTurn() {
            var turn = _playerTurns.Values.ElementAt(_turnIndex);

            if (turn is null)
                return null;

            _turnIndex++;

            if (_turnIndex >= _playerTurns.Count)
                _turnIndex = 0;

            return turn;
        }
    }
}