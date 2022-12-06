using System.Collections.Generic;
using _Project.Game.Player.Interfaces;

namespace _Project.Game.PlayerUtility.Interfaces {
    public interface IPlayerCreator {
        public IEnumerable<IPlayer> Generate(int playersToCreate);
    }
}