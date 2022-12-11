using UnityEngine;

namespace _Project.AppUI.Components.Draggable.Scripts {
    [RequireComponent(typeof(RectTransform))]
    public abstract class DraggableContainer : DraggableContainerBase {
        public virtual Vector3[] WorldContainerCorners {
            get {
                ContainerRectTransform.GetWorldCorners(containerCorners);
                return containerCorners;
            }
        }

        public RectTransform ContainerRectTransform {
            get { return _rectTransform ??= GetComponent<RectTransform>(); }
        }

        protected readonly Vector3[] containerCorners = new Vector3[4];

        RectTransform _rectTransform;
    }
}