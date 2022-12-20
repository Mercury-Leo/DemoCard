using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Core.SceneLoader.AddressableLoader.Interfaces {
    public interface IAddressableLoader {
        public Action OnLoadingFinished { get; set; }
        public IList<AsyncOperationHandle> AssetHandles { get; set; }
        AsyncOperationHandle<T> LoadAssetAsync<T>(string address);
    }
}