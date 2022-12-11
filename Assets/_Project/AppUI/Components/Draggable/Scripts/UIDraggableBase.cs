using System;
using _Project.AppUI.Components.Draggable.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.AppUI.Components.Draggable.Scripts {
    [RequireComponent(typeof(Selectable))]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIDraggableBase : MonoBehaviour, IUIDraggable, ISelectHandler {
        [field: SerializeField, SerializeReference]
        public DraggableContainerBase ContainerBase { get; set; }

        protected RectTransform DraggableRectTransform {
            get { return _rectTransform ??= GetComponent<RectTransform>(); }
        }

        protected Selectable selectable;
        protected CanvasGroup childCanvasGroup;
        RectTransform _rectTransform;

        public Action<Transform> OnObjectBeingHovered { get; set; }
        public Action<int> OnObjectBeginDrag { get; set; }
        public Action<Vector3> OnObjectBeingDragged { get; set; }
        public Action<int> OnObjectEndDrag { get; set; }

        protected virtual void Awake() {
            TryGetComponent(out selectable);
            TryGetComponent(out childCanvasGroup);
        }

        public abstract void OnDrag(PointerEventData eventData);
        public abstract void OnBeginDrag(PointerEventData eventData);
        public abstract void OnEndDrag(PointerEventData eventData);
        public abstract void OnSelect(BaseEventData eventData);
        public abstract void OnPointerEnter(PointerEventData eventData);

        protected virtual void PointerEnterHandler(Transform draggedItem) =>
            draggedItem.transform.SetSiblingIndex(transform.GetSiblingIndex());

        protected virtual void DragHandler(Vector3 position) {
            transform.position = position;
        }
    }
}