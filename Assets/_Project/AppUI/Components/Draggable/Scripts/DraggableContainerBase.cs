using System;
using _Project.AppUI.Components.Draggable.Interfaces;
using UnityEngine;

namespace _Project.AppUI.Components.Draggable.Scripts {
    public abstract class DraggableContainerBase : MonoBehaviour, IDraggableContainer {
        public GameObject CurrentlyDraggedItem { get; set; }

        Canvas IDraggableContainer.ParentCanvas {
            get { return _parentCanvas ??= GetComponent<Canvas>(); }
            set {
                if (value is null)
                    return;
                _parentCanvas = value;
            }
        }

        int IDraggableContainer.CurrentSiblingIndex {
            get => CurrentlyDraggedItem.transform.GetSiblingIndex();
            set {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        Canvas _parentCanvas;
    }
}