using System.Collections;
using MainGame.Services.AsyncRoutine.Interfaces;
using UnityEngine;

namespace MainGame.Services.AsyncRoutine
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