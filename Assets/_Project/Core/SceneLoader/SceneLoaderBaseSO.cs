using UnityEngine;

namespace _Project.Core.SceneLoader {
    public abstract class SceneLoaderBaseSO : ScriptableObject {
        protected bool hasLoaded;

        public abstract bool FinishedLoading();

        public abstract void ClearAssets();
    }
}