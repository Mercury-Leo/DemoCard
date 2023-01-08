using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Core.Dealer.Interfaces;
using _Project.Core.Dealer.Scripts;
using _Project.Core.SceneLoader.AddressableLoader.Scripts;
using _Project.Core.Singleton;
using _Project.Game.Player.Interfaces;
using _Project.Game.PlayerUtility.Interfaces;
using _Project.Game.PlayerUtility.Scripts;
using _Project.Game.TurnsHandler.Scripts;
using UnityEngine;
using static _Project.Game.GameConventions;

namespace _Project.Game {
    public class GameManager : SingletonBase<GameManager> {
        [SerializeField] AddressableLoaderBase _addressableLoader;

        [SerializeField] TurnHandler _turnsHandler;

        IPlayerCreator _playerCreator;
        IDeckCreator _deckCreator;
        IDeck _deck;

        List<IPlayer> _activePlayers;
        public Guid ActivePlayerID;

        public bool CanJoinGame { get; private set; }

        public Action OnGameStart { get; set; }
        public Action OnGameEnd { get; set; }
        public Action<List<IPlayer>> OnInitializePlayers { get; set; }
        public Action<IDeck> OnDeckDealt { get; set; }
        public Action<Guid> OnActivePlayerChanged { get; set; }

        void OnEnable() {
            _addressableLoader.OnLoadingFinished += LoadingFinished;
            _turnsHandler.OnPlayerTurn += PlayerTurn;
        }

        void OnDisable() {
            _addressableLoader.OnLoadingFinished -= LoadingFinished;
            _turnsHandler.OnPlayerTurn -= PlayerTurn;
        }

        public bool JoinPlayer(IPlayer cardPlayer) {
            if (_activePlayers.Count >= MaxPlayers)
                return false;
            _activePlayers.Add(cardPlayer);
            return true;
        }

        void LoadingFinished() {
            _activePlayers = new List<IPlayer>();
            _playerCreator = new PlayerCreator();
            _deckCreator = new DeckCreator();
            _deck = new Deck(_deckCreator);

            var players = _playerCreator.Generate(MaxPlayers);

            foreach (var player in players) {
                player.PopulateCards(_deck.Draw(StartingHand).ToList());
                _activePlayers.Add(player);
            }

            StartGame();
        }

        void StartGame() {
            CanJoinGame = false;
            OnInitializePlayers?.Invoke(_activePlayers);
            OnDeckDealt?.Invoke(_deck);
            _turnsHandler.AddPlayers(_activePlayers);
            _turnsHandler.StartTurns();
            OnGameStart?.Invoke();
        }

        void PlayerTurn(Guid id) {
            ActivePlayerID = id;
            OnActivePlayerChanged?.Invoke(ActivePlayerID);
        }
    }
}