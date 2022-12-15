using System;
using System.IO;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools {
    [Overlay(typeof(SceneView), "Scene Selection")]
    [Icon(Icon)]
    public class SceneSelectionOverlay : ToolbarOverlay {
        public const string Icon = "Assets/Editor/Icons/UnityIcon.png";

        SceneSelectionOverlay() : base(SceneDropdownToggle.ID) { }

        [EditorToolbarElement(ID, typeof(SceneView))]
        class SceneDropdownToggle : EditorToolbarDropdownToggle, IAccessContainerWindow {
            public const string ID = "SceneSelectionOverlay/SceneDropdownToggle";
            public EditorWindow containerWindow { get; set; }

            SceneDropdownToggle() {
                text = "Scenes";
                tooltip = "Select a scene to load";
                icon = AssetDatabase.LoadAssetAtPath<Texture2D>(Icon);

                dropdownClicked += ShowSceneMenu;
            }

            void ShowSceneMenu() {
                var menu = new GenericMenu();

                var scenes = EditorBuildSettings.scenes;

                var activeScene = SceneManager.GetActiveScene();

                var sceneGuids = AssetDatabase.FindAssets("t:scene", null);

                foreach (var t in sceneGuids) {
                    var path = AssetDatabase.GUIDToAssetPath(t);
                    var name = Path.GetFileNameWithoutExtension(path);
                    menu.AddItem(new GUIContent(name), string.CompareOrdinal(activeScene.name, name) == 0, () => OpenScene(activeScene, path));
                }
                
                menu.ShowAsContext();
            }

            void OpenScene(Scene activeScene, string path) {
                if (activeScene.isDirty) {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        EditorSceneManager.OpenScene(path);
                    return;
                }
                EditorSceneManager.OpenScene(path);
            }
            
        }
    }
}