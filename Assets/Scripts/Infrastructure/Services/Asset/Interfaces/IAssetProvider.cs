using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainGame.Infrastructure.Services.Asset.Interfaces
{
    public interface IAssetProvider
    {
        UniTask<GameObject> Instantiate(string address);
        UniTask<GameObject> Instantiate(string address, Vector3 at);
        UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation);
        UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent);
    }
}