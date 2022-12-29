using System;

namespace _Project.Core.TurnManager.Interfaces {
    public interface ITurn {
        public float TimeLimit { get; }
        public Guid UserID { get; }
    }
}