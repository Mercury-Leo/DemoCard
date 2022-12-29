using _Project.Core.Time.Interfaces;

namespace _Project.Core.Time.Scripts {
    public class MonoTimerProvider : ITimerProvider {
        public float GetTime() {
            return UnityEngine.Time.deltaTime;
        }
    }
}