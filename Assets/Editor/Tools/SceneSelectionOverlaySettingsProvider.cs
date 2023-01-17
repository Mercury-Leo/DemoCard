using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LeosClockworks.Editor.Tools {
    public class SceneSelectionOverlaySettingsProvider : SettingsProvider {
        public SceneSelectionOverlaySettingsProvider(string path, SettingsScope scopes,
            IEnumerable<string> keywords = null) : base(path, scopes, keywords) { }

        /// <summary>
        ///   <para>Use this function to draw the UI based on IMGUI. This assumes you haven't added any children to the rootElement passed to the OnActivate function.</para>
        /// </summary>
        /// <param name="searchContext">Search context for the Settings window. Used to show or hide relevant properties.</param>
        public override void OnGUI(string searchContext) {
            base.OnGUI(searchContext);

            GUILayout.Space(20f);

            var enabled = SceneSelectionOverlaySettings.instance.AdditiveOptionEnabled;
            var value = EditorGUILayout.Toggle("Additive Option", enabled, GUILayout.Width(200f));

            if (enabled != value)
                SceneSelectionOverlaySettings.instance.AdditiveOptionEnabled = value;
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider() {
            return new SceneSelectionOverlaySettingsProvider("Tools/Scene Selection Overlay", SettingsScope.Project);
        }
    }
}