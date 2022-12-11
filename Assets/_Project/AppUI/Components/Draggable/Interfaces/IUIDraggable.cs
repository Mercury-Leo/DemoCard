using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Interfaces {
    public interface IUIDraggable : IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {
        public abstract Action<GameObject> OnObjectBeingHovered { get; set; }
        public abstract Action<int> OnObjectBeginDrag { get; set; }
        public abstract Action<Vector3> OnObjectBeingDragged { get; set; }
        public abstract Action<int> OnObjectEndDrag { get; set; }
    }
}