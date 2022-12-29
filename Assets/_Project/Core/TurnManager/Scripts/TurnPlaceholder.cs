using System;
using _Project.Core.TurnManager.Interfaces;

namespace _Project.Core.TurnManager.Scripts {
    public class TurnPlaceholder : ITurn {
        public float TimeLimit { get; }
        public Guid UserID { get; }

        public TurnPlaceholder(float timeLimit, Guid userID) {
            TimeLimit = timeLimit;
            UserID = userID;
        }
    }
}