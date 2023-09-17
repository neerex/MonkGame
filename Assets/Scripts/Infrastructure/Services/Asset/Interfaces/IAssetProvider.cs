using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainGame.Infrastructure.Services.Asset.Interfaces
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string address);
        UniTask<GameObject> InstantiateAsync(string address);
        UniTask<GameObject> InstantiateAsync(string address, Vector3 at);
        UniTask<GameObject> InstantiateAsync(string address, Vector3 at, Quaternion rotation);
        UniTask<GameObject> InstantiateAsync(string address, Vector3 at, Quaternion rotation, Transform parent);
    }
}