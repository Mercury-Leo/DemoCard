using UnityEngine;

namespace _Project.Core.SceneLoader {
    public abstract class SceneLoaderBaseSO : ScriptableObject {
        protected bool hasLoaded;

        public virtual bool FinishedLoading() {
            return hasLoaded;
        }

        public abstract void ClearAssets();
    }
}