using System;

namespace _Project.Core.Card.Interfaces {
    public interface ICard {
        public int Value { get; set; }
        public CoreConventions.CardEffect CardEffect { get; set; }
        public Guid OwnerID { get; set; }
        public Action OnValueChanged { get; set; }
    }
}