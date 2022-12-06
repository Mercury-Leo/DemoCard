using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.AppUI.Components.Scripts {
    [RequireComponent(typeof(Button))]
    public class UIButton : UIObject {
        protected Button button;

        public Action OnButtonClicked { get; set; }

        protected virtual void Awake() {
            TryGetComponent(out button);
        }

        protected virtual void OnEnable() {
            button.onClick?.AddListener(ButtonClicked);
        }

        protected virtual void OnDisable() {
            button.onClick?.RemoveListener(ButtonClicked);
        }

        protected virtual void ButtonClicked() {
            OnButtonClicked?.Invoke();
        }
    }
}