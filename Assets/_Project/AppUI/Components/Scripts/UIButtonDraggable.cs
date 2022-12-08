using System;
using _Project.AppUI.Components.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Scripts {
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