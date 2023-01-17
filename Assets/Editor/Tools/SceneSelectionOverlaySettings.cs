using UnityEditor;
using UnityEngine;

namespace LeosClockworks.Editor.Tools {
    [FilePath("ProjectSettings/SceneSelectionOverlaySettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public class SceneSelectionOverlaySettings : ScriptableSingleton<SceneSelectionOverlaySettings> {
        [SerializeField] bool _additiveOptionEnabled;

        public bool AdditiveOptionEnabled {
            get => _additiveOptionEnabled;
            set {
                _additiveOptionEnabled = value;
                Save(true);
            }
        }
    }
}