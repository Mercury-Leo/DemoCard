using UnityEngine;

namespace _Project.Core.Card.Scripts {
    [CreateAssetMenu(fileName = "newCard", menuName = "Card")]
    public class CardSO : ScriptableObject {
        [SerializeField] private int _value;
        
    }
}