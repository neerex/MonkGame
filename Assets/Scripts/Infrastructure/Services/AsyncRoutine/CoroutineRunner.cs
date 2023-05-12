using System.Collections;
using MainGame.Infrastructure.Services.AsyncRoutine.Interfaces;
using UnityEngine;

namespace MainGame.Infrastructure.Services.AsyncRoutine
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine StartRoutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        public void StopRoutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }
    }
}