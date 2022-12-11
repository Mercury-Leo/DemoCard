using System;
using _Project.AppUI.Components.Draggable.Interfaces;
using _Project.AppUI.Components.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Scripts {
    public class UIButtonDraggable : UIButton, IUIDraggable {
        public Action<GameObject> OnObjectBeingHovered { get; set; }
        public Action<int> OnObjectBeginDrag { get; set; }
        public Action<Vector3> OnObjectBeingDragged { get; set; }
        public Action<int> OnObjectEndDrag { get; set; }

        public virtual void OnDrag(PointerEventData eventData) { }

        public virtual void OnBeginDrag(PointerEventData eventData) { }

        public virtual void OnEndDrag(PointerEventData eventData) { }

        public virtual void OnPointerEnter(PointerEventData eventData) { }
    }
}