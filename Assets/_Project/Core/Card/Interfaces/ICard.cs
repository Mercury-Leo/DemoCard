using System;

namespace _Project.Core.Card.Interfaces {
    public interface ICard {
        public abstract int Value { get; set; }
        public abstract CoreConventions.CardEffect CardEffect { get; set; }
        public abstract Guid OwnerID { get; set; }
    }
}