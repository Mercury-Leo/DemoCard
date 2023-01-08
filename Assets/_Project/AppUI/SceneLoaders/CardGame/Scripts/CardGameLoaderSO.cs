using _Project.AppUI.Card.Scripts;
using _Project.Core.SceneLoader;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.AppUI.SceneLoaders.CardGame.Scripts {
    [CreateAssetMenu(fileName = "new CardGameLoaderSO", menuName = "CardGameLoaderSO")]
    public class CardGameLoaderSO : SceneLoaderBaseSO {
        CardHandler _card;

        [ShowInInspector]
        public CardHandler Card {
            get => _card;

            set {
                if (value is null) {
                    _card = null;
                    hasLoaded = false;
                    return;
                }
                _card = value;
                hasLoaded = true;
            }
        }

        public override void ClearAssets() {
            Card = null;
        }
    }
}