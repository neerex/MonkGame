using System.Collections;
using UnityEngine;

namespace MainGame.Services.AsyncRoutine.Interfaces
{
    public interface ICoroutineRunner
    {
        Coroutine StartRoutine(IEnumerator enumerator);
        void StopRoutine(Coroutine routine);
    }
}