using System.Collections.Generic;
using System.Linq;
using _Project.Core.Dealer.Interfaces;
using _Project.Core.Dealer.Scripts;
using _Project.Game.Player.Interfaces;
using _Project.Game.PlayerUtility.Interfaces;
using _Project.Game.PlayerUtility.Scripts;
using UnityEngine;
using static _Project.Game.GameConventions;

namespace _Project.Game{
	public class GameManager : MonoBehaviour {
		IPlayerCreator _playerCreator;
		IDeckCreator _deckCreator;
		IDeck _deck;

		List<IPlayer> _activePlayers; 

		public bool CanJoinGame { get; private set; }
		
		void Awake() {
			_activePlayers = new List<IPlayer>();
			_playerCreator = new PlayerCreator();
			_deckCreator = new DeckCreator();
			_deck = new Deck(_deckCreator);
			var players = _playerCreator.Generate(3);

			foreach (var player in players) 
				player.Cards = _deck.Draw(StartingHand).ToList();
			
		}

		public bool JoinPlayer(IPlayer cardPlayer) {
			if (_activePlayers.Count >= MaxPlayers)
				return false;
			_activePlayers.Add(cardPlayer);
			return true;
		}

		private void StartGame() {
			CanJoinGame = false;
		}
	}
}