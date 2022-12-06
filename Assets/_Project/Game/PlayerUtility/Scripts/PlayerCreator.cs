using System;
using System.Collections.Generic;
using _Project.Game.Player.Interfaces;
using _Project.Game.Player.Scripts;
using _Project.Game.PlayerUtility.Interfaces;

namespace _Project.Game.PlayerUtility.Scripts {
    public class PlayerCreator : IPlayerCreator {
        public IEnumerable<IPlayer> Generate(int playersToCreate) {
            var players = new IPlayer[playersToCreate];
            for (var i = 0; i < playersToCreate; i++) {
                players[i] = new CardPlayer();
                players[i].PlayerID = new Guid();
            }
            return players;
        }
    }
}