using System;
using _Project.Game.Player.Interfaces;
using _Project.Game.Player.Scripts;
using _Project.Game.PlayerCreator.Interfaces;

namespace _Project.Game.PlayerCreator.Scripts {
    public class PlayerCreator : IPlayerCreator {
        public IPlayer[] GeneratePlayers(int playersToCreate) {
            var players = new IPlayer[playersToCreate];
            for (var i = 0; i < playersToCreate; i++) {
                players[i] = new CardPlayer();
                players[i].PlayerID = new Guid();
            }
            return players;
        }
    }
}