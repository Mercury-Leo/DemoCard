using System;
using Editor.Logger.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Scripts {
    public class UIDraggableObject : UIDraggableBase {
        BoundDraggableContainer Container {
            get { return _container ??= (BoundDraggableContainer)ContainerBase; }
        }

        BoundDraggableContainer _container;

        protected void OnEnable() {
            OnObjectBeingHovered += PointerEnterHandler;
            OnObjectBeingDragged += DragHandler;
        }

        protected void OnDisable() {
            OnObjectBeingHovered -= PointerEnterHandler;
            OnObjectBeingDragged -= DragHandler;
        }

        void LateUpdate() {
            if (Container.CurrentlyDraggedItem != gameObject)
                return;

            var input = GetInputPosition();

            if (input is null)
                return;

            OnObjectBeingDragged?.Invoke((Vector3)input);
        }

        Vector3? GetInputPosition() {
            Vector3? position;
#if UNITY_EDITOR || UNITY_STANDALONE // Editor/PC mouse input
            position = Input.mousePosition;
#else
            if (Input.touchCount <= 0 || Input.GetTouch(0).phase is not TouchPhase.Moved) return null;

            position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
#endif
            
            return position;
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            var draggedItem = Container.CurrentlyDraggedItem;

            if (draggedItem is null || draggedItem == gameObject)
                return;

            OnObjectBeingHovered?.Invoke(draggedItem.transform);
        }

        public override void OnBeginDrag(PointerEventData eventData) {
            Container.CurrentlyDraggedItem = gameObject;
            Container.UpdatedTransformPosition = transform.position;
            childCanvasGroup.blocksRaycasts = false;

            OnObjectBeginDrag?.Invoke(Container.CurrentSiblingIndex);
        }

        public override void OnDrag(PointerEventData eventData) {
            OnObjectBeingDragged?.Invoke(eventData.position);
        }

        public override void OnEndDrag(PointerEventData eventData) {
            if (Container.CurrentlyDraggedItem != gameObject)
                return;

            Container.CurrentlyDraggedItem.transform.position = Container.UpdatedTransformPosition;
            Container.CurrentlyDraggedItem = null;
            childCanvasGroup.blocksRaycasts = true;

            OnObjectEndDrag?.Invoke(Container.CurrentSiblingIndex);
        }

        public override void OnSelect(BaseEventData eventData) { }


        protected override void PointerEnterHandler(Transform draggedItem) {
            base.PointerEnterHandler(draggedItem);
            Container.UpdatedTransformPosition = transform.position;
        }
    }
}