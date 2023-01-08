using System;
using System.Collections.Generic;
using _Project.AppUI.Components.Draggable.Scripts;
using _Project.AppUI.Components.Scripts;
using _Project.AppUI.SceneLoaders.CardGame.Scripts;
using _Project.Core.Card.Interfaces;
using Editor.Logger.Scripts;
using UnityEngine;

namespace _Project.AppUI.Deck.Scripts {
    public abstract class CardPileBase : UIButton {
        [SerializeField] protected Transform _draggedZone;
        [SerializeField] protected DraggableContainerBase _container;

        [Header("Loader")] [SerializeField] protected CardGameLoaderSO _loader;

        public Action<ICard> OnCardDrew { get; set; }
        public Action OnPileEmpty { get; set; }
        
        public bool CanDraw { get; set; }

        protected Stack<ICard> PileCards;

        protected override void OnEnable() {
            base.OnEnable();
            OnButtonClicked += DrawCard;
        }

        protected override void OnDisable() {
            base.OnDisable();
            OnButtonClicked -= DrawCard;
        }

        protected abstract void DrawCard();

        public virtual void SetPile(Stack<ICard> cards) {
            if (cards is null)
                return;
            if (cards.Count <= 0)
                return;
            PileCards = cards;
        }

        public virtual void AddCard(ICard card) {
            if (card is null)
                return;
            PileCards.Push(card);
        }

        protected ICard GetCard() {
            if (PileCards is null)
                return null;

            if (!CanDraw)
                return null;

            if (PileCards.TryPop(out var cardResult)) return cardResult;

            OnPileEmpty?.Invoke();
            this.Log("Failed to pop card");
            return null;
        }

        protected void CreateCard(ICard card) {
            var cardHandler = Instantiate(_loader.Card, _draggedZone);
            cardHandler.SetContainer(_container);

            cardHandler.SetCardData(card);
            cardHandler.ShowValue = true;
            OnCardDrew?.Invoke(card);
        }
    }
}