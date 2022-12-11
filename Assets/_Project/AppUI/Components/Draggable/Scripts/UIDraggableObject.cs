using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Scripts {
    public class UIDraggableObject : UIDraggableBase {

        BoundDraggableContainer _container;
        
        protected Vector3[] ContainerCorners;
        protected (float x, float y) ObjectBounds;

        protected override void Awake() {
            base.Awake();
            _container = (BoundDraggableContainer)ContainerBase;
        }

        protected void Start() {
            ContainerCorners = _container.WorldContainerCorners;
        }

        protected void OnEnable() {
            OnObjectBeingHovered += PointerEnterHandler;
        }

        protected void OnDisable() {
            throw new NotImplementedException();
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            var draggedItem = _container.CurrentlyDraggedItem;

            if (draggedItem is null || draggedItem == gameObject)
                return;
            
            OnObjectBeingHovered?.Invoke(draggedItem);
        }
        
        public override void OnBeginDrag(PointerEventData eventData) {
            _container.CurrentlyDraggedItem = gameObject;
            _container.UpdatedTransformPosition = transform.position;
            childCanvasGroup.blocksRaycasts = false;
            
            OnObjectBeginDrag?.Invoke(_container.CurrentSiblingIndex);
        }

        public override void OnDrag(PointerEventData eventData) {
            throw new NotImplementedException();
        }

        public override void OnEndDrag(PointerEventData eventData) {
            throw new NotImplementedException();
        }

        public override void OnSelect(BaseEventData eventData) {
            throw new NotImplementedException();
        }

     

        protected override void PointerEnterHandler(Transform draggedItem) {
            base.PointerEnterHandler(draggedItem);
            _container.UpdatedTransformPosition = transform.position;
        }
    }
}