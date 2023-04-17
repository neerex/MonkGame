using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainGame.Services.Asset.Interfaces
{
    public interface IInjectedAssetProvider
    {
        GameObject Instantiate(GameObject prefab, Transform parent = null);
        UniTask<GameObject> Instantiate(string address);
        UniTask<GameObject> Instantiate(string address, Vector3 at);
        UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation);
        UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent);
    }
}