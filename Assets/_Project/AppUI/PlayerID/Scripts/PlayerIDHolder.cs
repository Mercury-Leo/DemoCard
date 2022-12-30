using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.AppUI.PlayerID.Scripts {
    public class PlayerIDHolder : MonoBehaviour {
        [ShowInInspector] public Guid PlayerID { get; private set; }

        public bool IsActivePlayer {
            get => _isActivePlayer;
            set {
                _isActivePlayer = value;
                if (value)
                    OnPlayerActive?.Invoke(PlayerID);
                if(!value)
                    OnPlayerDeActive?.Invoke(PlayerID);
            }
        }

        public Action<Guid> OnPlayerActive { get; set; }
        
        public Action<Guid> OnPlayerDeActive { get; set; }

        bool _isActivePlayer;

        public void SetID(Guid playerID) {
            PlayerID = playerID;
        }
    }
}