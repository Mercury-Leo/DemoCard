using System;
using _Project.Core.Singleton;
using _Project.Core.Time.Scripts;
using _Project.Core.TurnManager.Interfaces;
using UnityEngine;
using static _Project.Core.CoreConventions;

namespace _Project.Core.TurnManager.Scripts {
    public class TurnManager : SingletonBase<TurnManager> {
        readonly InternalTimer _timer = new(TimeProvider);

        Coroutine _timerRoutine;

        ITurn _currentTurn;

        public Action OnTurnBegin { get; set; }
        public Action OnTurnEnded { get; set; }
        public Action OnTurnLose { get; set; }

        void OnEnable() {
            _timer.OnTimerFinished += OnTimerFinished;
            _timer.OnTimerBegin += OnTimerBegin;
            _timer.OnTimerEnd += OnTimerEnd;
        }

        void OnDisable() {
            _timer.OnTimerFinished -= OnTimerFinished;
            _timer.OnTimerBegin -= OnTimerBegin;
            _timer.OnTimerEnd -= OnTimerEnd;
        }

        public void StartTurn(ITurn turn) {
            if (turn is null)
                return;

            _currentTurn = turn;
            _timerRoutine = StartCoroutine(_timer.C_RunTimer(turn.TimeLimit));
        }

        public void RestartTurn() {
            if (_currentTurn is null)
                return;

            _timer.AddTime(_currentTurn.TimeLimit, true);
        }

        public void EndTurn() {
            if (_timerRoutine is null)
                return;

            StopCoroutine(_timerRoutine);
            _timer.StopTimer();
        }

        public void NextTurn(ITurn turn) {
            EndTurn();
            StartTurn(turn);
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