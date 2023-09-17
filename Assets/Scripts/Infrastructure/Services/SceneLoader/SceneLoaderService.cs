using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGame.Infrastructure.Services.SceneLoader
{
    public class SceneLoaderService : ISceneLoadService
    {
        public async UniTask Load(string nextScene)
        {
            if (SceneManager.GetActiveScene().name != nextScene)
            {
                AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
                await waitNextScene.ToUniTask();
            }
        }
    }
}