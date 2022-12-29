using System;
using _Project.Core.Time.Interfaces;
using UnityEngine;

namespace _Project.Core.Time.Scripts {
    public class TimeManager {
        float TimeCounter {
            get => _timeCounter;
            set {
                _timeCounter = value;
                CurrentTime?.Invoke(value);
            }
        }

        bool _isTimerCounting;
        float _timeCounter;
        readonly ITimerProvider _timerProvider;

        public float TimeToCount { get; set; } = 60f;

        public Action OnTimerBegin { get; set; }
        public Action OnTimerEnd { get; set; }
        public Action OnTimerFinished { get; set; }
        public Action<float> CurrentTime { get; set; }

        public TimeManager(ITimerProvider timerProvider) {
            _timerProvider = timerProvider;
        }

        public void BeginTimer() {
            Debug.Log("Timer Running");
            SetTimer();
            _isTimerCounting = true;
            OnTimerBegin?.Invoke();
        }

        public void StopTimer() {
            TimeCounter = 0;
            _isTimerCounting = false;
            OnTimerEnd?.Invoke();
        }

        /// <summary>
        /// Runs the timer, in update for example
        /// </summary>
        public void RunTimer() {
            if (!_isTimerCounting)
                return;

            if (TimeCounter > 0) {
                TimeCounter -= _timerProvider.GetTime();
                return;
            }

            TimeCounter = 0;
            OnTimerFinished?.Invoke();
        }

        void SetTimer() {
            TimeCounter = TimeToCount;
        }
    }
}