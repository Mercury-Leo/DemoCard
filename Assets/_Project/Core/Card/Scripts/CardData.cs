using System;
using _Project.Core.Card.Interfaces;
using _Project.Core.Extensions.Card;

namespace _Project.Core.Card.Scripts {
    public class CardData : ICard {
        #region Properties

        public int Value { get; set; }
        public CoreConventions.CardEffect CardEffect { get; set; }
        public Guid OwnerID { get; set; }

        #endregion

        #region Constructor

        public CardData(int value) {
            Value = value;
            CardEffect = CardExtensions.ConvertToCardEffect(value);
        }

        #endregion
    }
}