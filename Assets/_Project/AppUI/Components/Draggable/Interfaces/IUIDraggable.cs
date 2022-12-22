using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.AppUI.Components.Draggable.Interfaces {
    public interface IUIDraggable : IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler,
        IEndDragHandler, IDropHandler {
        public Action<Transform> OnObjectEnterHovered { get; set; }
        public Action OnObjectExitHover { get; set; }
        public Action<Transform> OnObjectBeginDrag { get; set; }
        public Action<Vector3> OnObjectBeingDragged { get; set; }
        public Action OnObjectEndDrag { get; set; }
    }
}