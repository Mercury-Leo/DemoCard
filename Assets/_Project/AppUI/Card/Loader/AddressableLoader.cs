using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.AppUI.Card.Loader {
    public static class AddressableLoader {
        static AsyncOperationHandle<GameObject> _handle;

        public static Action<GameObject> OnGameObjectLoaded { get; set; }

        public static void GetObjectByAddress(string address) {
            _handle = Addressables.LoadAssetAsync<GameObject>(address);
            _handle.Completed += GameObjectCreated;
        }

        public static void GetObjectByReference(AssetReference reference) {
            _handle = reference.LoadAssetAsync<GameObject>();
            _handle.Completed += GameObjectCreated;
        }

        static void GameObjectCreated(AsyncOperationHandle<GameObject> handle) {
            if (handle.Status is AsyncOperationStatus.Succeeded) {
                OnGameObjectLoaded?.Invoke(handle.Result);
                CleanUp();
                return;
            }

            Debug.LogError("Failed to load game object");
        }

        static void CleanUp() {
            Addressables.Release(_handle);
        }
    }
}