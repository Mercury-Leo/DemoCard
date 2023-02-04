using UnityEngine;

namespace _Project.Core.Singleton {
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour {
        static T _instance;

        public static T Instance {
            get {
                if (_instance is not null) return _instance;
                _instance = FindObjectOfType<T>();

                if (_instance is not null) return _instance;
                var go = new GameObject();
                _instance = go.AddComponent<T>();

                return _instance;
            }
            private set {
                if (value is null)
                    return;
                _instance = value;
            }
        }

        protected virtual void Awake() {
            if (Instance is not null && Instance != this) {
                Destroy(this);
                return;
            }
            
            Instance = this as T;
        }
    }
}