using _Project.Core.Placeholders;
using TMPro;
using UnityEngine;

namespace _Project.AppUI.Components.Scripts {
    [RequireComponent(typeof(TMP_Text))]
    public class UIText : MonoBehaviour {
        TMP_Text _textComponent;

        string Text {
            get => _textComponent.text;
            set {
                if (string.IsNullOrEmpty(value))
                    return;

                _textComponent.text = value;
            }
        }

        void Awake() {
            TryGetComponent(out _textComponent);
        }

        public void SetText(string text) {
            Text = text;
        }

        public void SetText(TextPlaceholder placeholder) {
            Text = placeholder.GetText();
        }
    }
}