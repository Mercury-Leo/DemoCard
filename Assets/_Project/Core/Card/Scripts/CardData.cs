using System;
using _Project.Core.Card.Interfaces;
using _Project.Core.Extensions.Card;

namespace _Project.Core.Card.Scripts {
    public class CardData : ICard {
        #region Properties

        public int Value {
            get => _value;
            set {
                _value = value;
                CardEffect = CardExtensions.ConvertToCardEffect(value);
                OnValueChanged?.Invoke();
            }
        }

        public CoreConventions.CardEffect CardEffect { get; set; }
        public Guid OwnerID { get; set; }
        
        public Action OnValueChanged { get; set; }

        int _value;

        #endregion

        #region Constructor

        public CardData(int value) {
            Value = value;
        }

        #endregion
    }
}