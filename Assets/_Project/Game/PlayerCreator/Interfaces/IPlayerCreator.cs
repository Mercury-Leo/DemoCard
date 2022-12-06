using _Project.Game.Player.Interfaces;

namespace _Project.Game.PlayerCreator.Interfaces {
    public interface IPlayerCreator {
        public IPlayer[] GeneratePlayers(int playersToCreate);
    }
}