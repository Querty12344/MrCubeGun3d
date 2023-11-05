using System;

namespace Infrastructure.SceneManagment
{
    public interface ISceneLoader
    {
        void Load(string sceneName, Action onLoaded = null);
    }
}