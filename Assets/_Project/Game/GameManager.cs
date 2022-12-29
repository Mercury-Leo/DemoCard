using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Core.Dealer.Interfaces;
using _Project.Core.Dealer.Scripts;
using _Project.Core.SceneLoader.AddressableLoader.Scripts;
using _Project.Core.TurnManager.Scripts;
using _Project.Game.Player.Interfaces;
using _Project.Game.PlayerUtility.Interfaces;
using _Project.Game.PlayerUtility.Scripts;
using UnityEngine;
using static _Project.Game.GameConventions;

namespace _Project.Game {
    public class GameManager : MonoBehaviour {

        [SerializeField] AddressableLoaderBase _addressableLoader;

        [SerializeField] TurnManager _turnManager;
        
        IPlayerCreator _playerCreator;
        IDeckCreator _deckCreator;
        IDeck _deck;

        List<IPlayer> _activePlayers;

        public bool CanJoinGame { get; private set; }

        public Action OnGameStart { get; set; }
        public Action OnGameEnd { get; set; }
        public Action<List<IPlayer>> OnInitializePlayers { get; set; }
        public Action<IDeck> OnDeckDealt { get; set; }
        
        void OnEnable() {
            _addressableLoader.OnLoadingFinished += OnLoadingFinished;
        }

        void OnDisable() {
            _addressableLoader.OnLoadingFinished -= OnLoadingFinished;
        }

        void OnLoadingFinished() {
            _activePlayers = new List<IPlayer>();
            _playerCreator = new PlayerCreator();
            _deckCreator = new DeckCreator();
            _deck = new Deck(_deckCreator);
            
            var players = _playerCreator.Generate(5);
            
            foreach (var player in players) {
                player.PopulateCards(_deck.Draw(StartingHand).ToList());
                _activePlayers.Add(player);
            }
            
            StartGame();
        }

        public bool JoinPlayer(IPlayer cardPlayer) {
            if (_activePlayers.Count >= MaxPlayers)
                return false;
            _activePlayers.Add(cardPlayer);
            return true;
        }

        public void StartGame() {
            CanJoinGame = false;
            OnInitializePlayers?.Invoke(_activePlayers);
            OnDeckDealt?.Invoke(_deck);
            _turnManager.BeginTurn();
        }
    }
}