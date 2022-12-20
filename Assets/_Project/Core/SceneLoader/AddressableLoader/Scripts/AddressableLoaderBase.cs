using System;
using System.Collections.Generic;
using _Project.Core.SceneLoader.AddressableLoader.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Core.SceneLoader.AddressableLoader.Scripts {
    public abstract class AddressableLoaderBase : MonoBehaviour, IAddressableLoader {

        [SerializeField] protected SceneLoaderBaseSO _loader;
        public Action OnLoadingFinished { get; set; }
        public IList<AsyncOperationHandle> AssetHandles { get; set; } = new List<AsyncOperationHandle>();

        protected virtual void Awake() {
            Addressables.InitializeAsync().Completed += AddressablesInitialized;
        }

        protected void OnDestroy() {
            foreach (var handle in AssetHandles) {
                Addressables.Release(handle);
            }
            
            _loader.ClearAssets();
        }

        protected abstract void AddressablesInitialized(AsyncOperationHandle<IResourceLocator> obj);

        public AsyncOperationHandle<T> LoadAssetAsync<T>(string address) {
            var loadHandle = Addressables.LoadAssetAsync<T>(address);

            return loadHandle;
        }
    }
}