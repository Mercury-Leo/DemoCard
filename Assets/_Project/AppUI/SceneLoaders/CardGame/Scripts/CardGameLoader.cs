using _Project.AppUI.Card.Scripts;
using _Project.Core.SceneLoader.AddressableLoader.Scripts;
using Editor.Logger.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.AppUI.SceneLoaders.CardGame.Scripts {
    public class CardGameLoader : AddressableLoaderBase {
        [SerializeField] CardGameLoaderSO _cardLoader;

        [Header("Addresses")] [SerializeField] AssetReference _cardPrefab;

        protected override void Awake() {
            base.Awake();
            _cardLoader = (CardGameLoaderSO)_loader;
        }

        protected override void AddressablesInitialized(AsyncOperationHandle<IResourceLocator> obj) {
            CardHandlerLoader();
        }

        void CardHandlerLoader() {
            if (_loader.FinishedLoading())
                return;

            var card = LoadAssetAsync<GameObject>(_cardPrefab.AssetGUID);

            card.Completed += handle => {
                if (handle.Status is not AsyncOperationStatus.Succeeded) {
                    this.LogError("Failed to load card asset");
                    return;
                }

                AssetHandles.Add(handle);

                var cardAsset = handle.Result.GetComponent<CardHandler>();

                if (cardAsset is null)
                    return;

                _cardLoader.Card = cardAsset;
                OnLoadingFinished?.Invoke();
            };
        }
    }
}