using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Scripts {
    public class UIDraggableObject : UIDraggableBase {
        BoundDraggableContainer _container;

        protected override void Awake() {
            base.Awake();
            _container = (BoundDraggableContainer)ContainerBase;
        }
        
        protected void OnEnable() {
            OnObjectBeingHovered += PointerEnterHandler;
        }

        protected void OnDisable() {
            OnObjectBeingHovered -= PointerEnterHandler;
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            var draggedItem = _container.CurrentlyDraggedItem;

            if (draggedItem is null || draggedItem == gameObject)
                return;

            OnObjectBeingHovered?.Invoke(draggedItem.transform);
        }

        public override void OnBeginDrag(PointerEventData eventData) {
            _container.CurrentlyDraggedItem = gameObject;
            _container.UpdatedTransformPosition = transform.position;
            childCanvasGroup.blocksRaycasts = false;

            OnObjectBeginDrag?.Invoke(_container.CurrentSiblingIndex);
        }

        public override void OnDrag(PointerEventData eventData) {
            OnObjectBeingDragged?.Invoke(eventData.position);
        }

        public override void OnEndDrag(PointerEventData eventData) {
            if (_container.CurrentlyDraggedItem != gameObject)
                return;

            _container.CurrentlyDraggedItem.transform.position = _container.UpdatedTransformPosition;
            _container.CurrentlyDraggedItem = null;
            childCanvasGroup.blocksRaycasts = true;

            OnObjectEndDrag?.Invoke(_container.CurrentSiblingIndex);
        }

        public override void OnSelect(BaseEventData eventData) { }


        protected override void PointerEnterHandler(Transform draggedItem) {
            base.PointerEnterHandler(draggedItem);
            _container.UpdatedTransformPosition = transform.position;
        }
    }
}