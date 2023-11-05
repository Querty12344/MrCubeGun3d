using System;
using System.Collections;
using Infrastructure.GameCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagment
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));
        }

        private IEnumerator LoadScene(string sceneName, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            var loadScene = SceneManager.LoadSceneAsync(sceneName);
            while (loadScene.isDone == false) yield return null;
            onLoaded?.Invoke();
        }
    }
}