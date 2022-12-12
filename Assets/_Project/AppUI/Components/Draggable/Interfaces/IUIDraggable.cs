using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Interfaces {
    public interface IUIDraggable : IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerExitHandler {
        public abstract Action<Transform> OnObjectEnterHovered { get; set; }
        public abstract Action OnObjectExitHover { get; set; }
        public abstract Action<int> OnObjectBeginDrag { get; set; }
        public abstract Action<Vector3> OnObjectBeingDragged { get; set; }
        public abstract Action<int> OnObjectEndDrag { get; set; }
    }
}