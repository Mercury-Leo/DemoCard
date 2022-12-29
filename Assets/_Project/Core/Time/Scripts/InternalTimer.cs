using System;
using System.Collections;
using _Project.Core.Time.TimeProvider.Interfaces;
using UnityEngine;

namespace _Project.Core.Time.Scripts {
    public class InternalTimer {
        float _timeCounter;
        readonly ITimeProvider _timeProvider;

        float TimeCounter {
            get => _timeCounter;
            set {
                _timeCounter = value;
                CurrentTime?.Invoke(value);
            }
        }

        public Action OnTimerBegin { get; set; }
        public Action OnTimerEnd { get; set; }
        public Action OnTimerFinished { get; set; }
        public Action<float> CurrentTime { get; set; }

        public InternalTimer(ITimeProvider timeProvider) {
            _timeProvider = timeProvider;
        }

        public void StopTimer() {
            TimeCounter = 0;
            OnTimerEnd?.Invoke();
        }

        public void AddTime(float time, bool reset = false) {
            if (time <= 0)
                return;

            if (reset)
                TimeCounter = 0;
            
            TimeCounter += time;
        }

        public IEnumerator C_RunTimer(float timeLimit) {
            if (timeLimit <= 0)
                yield return null;

            TimeCounter = timeLimit;

            OnTimerBegin?.Invoke();

            while (TimeCounter > 0) {
                TimeCounter -= _timeProvider.GetTime();
                yield return new WaitForSeconds(_timeProvider.GetTime());
            }

            TimeCounter = 0;
            OnTimerFinished?.Invoke();
        }
    }
}