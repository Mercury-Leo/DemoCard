using System;
using _Project.Core.Enums.CardEffects;

namespace _Project.Core.Card.Interfaces {
    public interface ICard {
        public int Value { get; set; }
        public CardEffects CardEffect { get; set; }
        public Guid OwnerID { get; set; }
        public Action OnValueChanged { get; set; }
    }
}