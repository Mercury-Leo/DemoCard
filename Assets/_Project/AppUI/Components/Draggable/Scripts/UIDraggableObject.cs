using _Project.Core.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Scripts {
    public class UIDraggableObject : UIDraggableBase {

        Transform _draggedParent;
        int _draggedIndex;
        
        BoundDraggableContainer Container {
            get { return _container ??= (BoundDraggableContainer)ContainerBase; }
        }

        BoundDraggableContainer _container;

        protected void OnEnable() {
            OnObjectEnterHovered += PointerEnterHandler;
            OnObjectBeingDragged += DragHandler;
        }

        protected void OnDisable() {
            OnObjectEnterHovered -= PointerEnterHandler;
            OnObjectBeingDragged -= DragHandler;
        }

        void LateUpdate() {
            if (Container.CurrentlyDraggedItem != gameObject)
                return;

            var input = MouseInput.GetPosition();

            if (input is null)
                return;

            OnObjectBeingDragged?.Invoke((Vector3)input);
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            var draggedItem = Container.CurrentlyDraggedItem;

            if (draggedItem is null || draggedItem == gameObject)
                return;

            OnObjectEnterHovered?.Invoke(draggedItem.transform);
        }

        public override void OnPointerExit(PointerEventData eventData) {
            OnObjectExitHover?.Invoke();
        }

        public override void OnDrop(PointerEventData eventData) {
            var draggedItem = Container.CurrentlyDraggedItem;

            if (draggedItem is null || draggedItem == gameObject)
                return;
            
            OnObjectBeingDropped?.Invoke(draggedItem.transform);
        }

        public override void OnBeginDrag(PointerEventData eventData) {
            Container.CurrentlyDraggedItem = gameObject;
            Container.UpdatedTransformPosition = transform.position;
            childCanvasGroup.blocksRaycasts = false;

            OnObjectBeginDrag?.Invoke(Container.CurrentlyDraggedItem.transform);
            _draggedParent = transform.parent;
            _draggedIndex = transform.GetSiblingIndex();
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
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
            transform.SetParent(_draggedParent);
            transform.SetSiblingIndex(_draggedIndex);
            
            OnObjectEndDrag?.Invoke();
        }

        public override void OnSelect(BaseEventData eventData) { }


        protected override void PointerEnterHandler(Transform draggedItem) {
            //base.PointerEnterHandler(draggedItem);
            //Container.UpdatedTransformPosition = transform.position;
        }
    }
}