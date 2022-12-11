using UnityEngine;

namespace _Project.Core.Input {
    public static class MouseInput {
        public static Vector3? GetPosition() {
            Vector3? position;
#if UNITY_EDITOR || UNITY_STANDALONE // Editor/PC mouse input
            position = UnityEngine.Input.mousePosition;
#else
            if (Input.touchCount <= 0 || Input.GetTouch(0).phase is not TouchPhase.Moved) return null;

            position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
#endif

            return position;
        }
    }
}