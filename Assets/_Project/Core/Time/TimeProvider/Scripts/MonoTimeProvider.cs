using _Project.Core.Time.TimeProvider.Interfaces;

namespace _Project.Core.Time.Scripts {
    public class MonoTimeProvider : ITimeProvider {
        public float GetTime() {
            return UnityEngine.Time.deltaTime;
        }
    }
}