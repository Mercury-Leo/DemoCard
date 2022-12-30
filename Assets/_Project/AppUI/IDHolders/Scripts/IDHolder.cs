using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.AppUI.IDHolders.Scripts {
    public class IDHolder : MonoBehaviour {
        [ShowInInspector] public Guid PlayerID { get; private set; }

        public bool IsActivePlayer {
            get => _isActivePlayer;
            set {
                _isActivePlayer = value;
                if (value)
                    OnPlayerActive?.Invoke(PlayerID, true);
                if(!value)
                    OnPlayerActive?.Invoke(PlayerID, false);
            }
        }

        public Action<Guid, bool> OnPlayerActive { get; set; }
        
        bool _isActivePlayer;

        public void SetID(Guid playerID) {
            PlayerID = playerID;
        }
    }
}