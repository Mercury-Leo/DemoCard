using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace _Project.Core.Extensions.MonoBehaviour {
    /// <summary>
    /// Based on <see>
    ///     <cref>https://forum.unity.com/threads/startcoroutine-add-overload-with-cancellationtoken.1006379/</cref>
    /// </see>
    /// </summary>
    public static class MonoBehaviourExtensions {
        static IEnumerator CoroutineCancel(IEnumerator routine, CancellationToken token) {
            if (routine is null)
                throw new ArgumentException(nameof(routine));

            if (token == CancellationToken.None)
                throw new ArgumentException(nameof(token));

            while (!token.IsCancellationRequested && routine.MoveNext())
                yield return routine.Current;
        }

        public static Coroutine StartCoroutine(this UnityEngine.MonoBehaviour monoBehaviour, IEnumerator routine,
            CancellationToken token) {
            return monoBehaviour.StartCoroutine(CoroutineCancel(routine, token));
        }
    }
}