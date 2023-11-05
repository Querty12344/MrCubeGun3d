using System.Collections;
using UnityEngine;

namespace Infrastructure.GameCore
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
        void StopAllCoroutines();
    }
}