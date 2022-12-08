using System;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Scripts {
    public class UIDraggable : UIDraggableBase {
        protected virtual void OnEnable() {
            throw new NotImplementedException();
        }

        protected virtual void OnDisable() {
            throw new NotImplementedException();
        }

        public override void OnDrag(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public override void OnBeginDrag(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public override void OnEndDrag(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        public override void OnSelect(BaseEventData eventData) {
            throw new System.NotImplementedException();
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            var draggedItem = 0;
        }
    }
}