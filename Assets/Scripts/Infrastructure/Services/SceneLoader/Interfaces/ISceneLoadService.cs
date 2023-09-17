using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace MainGame.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoadService
    {
        UniTask Load(string nextScene);
    }
}