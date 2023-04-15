using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainGame.Services.Asset
{
    public class AssetProvider : IAssetProvider
    {
        public async UniTask<GameObject> Instantiate(string address)
        {
            GameObject prefab = await Resources.LoadAsync<GameObject>(address) as GameObject;
            return Object.Instantiate(prefab);
        }

        public async UniTask<GameObject> Instantiate(string address, Vector3 at)
        {
            GameObject prefab = await Resources.LoadAsync<GameObject>(address) as GameObject;
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public async UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation)
        {
            GameObject prefab = await Resources.LoadAsync<GameObject>(address) as GameObject;
            return Object.Instantiate(prefab, at, rotation);
        }

        public async UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent)
        {
            GameObject prefab = await Resources.LoadAsync<GameObject>(address) as GameObject;
            return Object.Instantiate(prefab, at, rotation, parent);
        }
    }
}