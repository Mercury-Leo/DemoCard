using _Project.Core.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Scripts {
    public class UIDraggableObject : UIDraggableBase {
        Transform _draggedParent;
        int _draggedIndex;

        public override bool CanBeDragged {
            get => _canBeDragged;
            set {
                _canBeDragged = value;
                ReturnToBase();
            }
        }

        bool _canBeDragged;

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
            if (!CanBeDragged)
                return;

            if (Container.CurrentlyDraggedItem != gameObject)
                return;

            var input = MouseInput.GetPosition();

            if (input is null)
                return;

            OnObjectBeingDragged?.Invoke((Vector3)input);
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            if (!CanBeDragged)
                return;

            var draggedItem = Container.CurrentlyDraggedItem;

            if (draggedItem is null || draggedItem == gameObject)
                return;

            OnObjectEnterHovered?.Invoke(draggedItem.transform);
        }

        public override void OnPointerExit(PointerEventData eventData) {
            if (!CanBeDragged)
                return;

            OnObjectExitHover?.Invoke();
        }

        public override void OnDrop(PointerEventData eventData) {
            if (!CanBeDragged)
                return;

            var draggedItem = Container.CurrentlyDraggedItem;

            if (draggedItem is null || draggedItem == gameObject)
                return;

            OnObjectBeingDropped?.Invoke(draggedItem.transform);
        }

        public override void OnBeginDrag(PointerEventData eventData) {
            if (!CanBeDragged)
                return;

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
            if (!CanBeDragged)
                return;

            OnObjectBeingDragged?.Invoke(eventData.position);
        }

        public override void OnEndDrag(PointerEventData eventData) {
            if (!CanBeDragged)
                return;

            EndDrag();
        }

        public override void OnSelect(BaseEventData eventData) {
            if (!CanBeDragged)
                return;
        }


        protected override void PointerEnterHandler(Transform draggedItem) {
            if (!CanBeDragged)
                return;

            //base.PointerEnterHandler(draggedItem);
            //Container.UpdatedTransformPosition = transform.position;
        }

        void EndDrag() {
            if (Container.CurrentlyDraggedItem != gameObject)
                return;

            Container.CurrentlyDraggedItem.transform.position = Container.UpdatedTransformPosition;
            Container.CurrentlyDraggedItem = null;
            childCanvasGroup.blocksRaycasts = true;
            transform.SetParent(_draggedParent);
            transform.SetSiblingIndex(_draggedIndex);

            OnObjectEndDrag?.Invoke();
        }

        void ReturnToBase() {
            EndDrag();
        }
    }
}