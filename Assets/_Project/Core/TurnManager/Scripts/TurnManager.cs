using System;
using _Project.Core.Time.Scripts;
using UnityEngine;
using static _Project.Core.CoreConventions;

namespace _Project.Core.TurnManager.Scripts {
    public class TurnManager : MonoBehaviour {
        readonly TimeManager _timeManager = new(TimerProvider);

        public Action OnTurnBegin { get; set; }
        public Action OnTurnEnded { get; set; }
        public Action OnTurnLose { get; set; }

        void Awake() {
            _timeManager.TimeToCount = 60;
        }

        void OnEnable() {
            _timeManager.OnTimerFinished += OnTimerFinished;
            _timeManager.OnTimerBegin += OnTimerBegin;
            _timeManager.OnTimerEnd += OnTimerEnd;
        }

        void OnDisable() {
            _timeManager.OnTimerFinished -= OnTimerFinished;
            _timeManager.OnTimerBegin -= OnTimerBegin;
            _timeManager.OnTimerEnd -= OnTimerEnd;
        }

        void Update() {
            _timeManager.RunTimer();
        }

        public void BeginTurn() {
            _timeManager.BeginTimer();
        }

        public void RestartTurn() {
            _timeManager.BeginTimer();
        }

        public void StopTurns() {
            _timeManager.StopTimer();
        }

        void OnTimerFinished() {
            OnTurnLose?.Invoke();
        }

        void OnTimerBegin() {
            OnTurnBegin?.Invoke();
        }

        void OnTimerEnd() {
            OnTurnEnded?.Invoke();
        }
    }
}