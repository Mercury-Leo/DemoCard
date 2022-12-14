using UnityEngine;

namespace _Project.AppUI.Components.Draggable.Interfaces {
    public interface IDraggableContainer {
        public GameObject CurrentlyDraggedItem { get; set; }

        public Canvas ParentCanvas { get; set; }
        
        public int CurrentSiblingIndex { get; set; }
    }
}