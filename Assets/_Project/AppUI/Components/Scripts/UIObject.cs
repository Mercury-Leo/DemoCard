using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.AppUI.Components.Scripts {
    [RequireComponent(typeof(Selectable))]
    public abstract class UIObject : MonoBehaviour, ISelectHandler {
        public Action<UIObject> OnSelected { get; set; }

        public void OnSelect(BaseEventData eventData) {
            OnSelected?.Invoke(this);
        }
    }
}