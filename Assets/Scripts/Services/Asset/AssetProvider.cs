using UnityEngine;

namespace MainGame.Services.Asset
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string address)
        {
            GameObject prefab = Resources.Load<GameObject>(address);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string address, Vector3 at) 
        {
            GameObject prefab = Resources.Load<GameObject>(address);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string address, Vector3 at, Quaternion rotation)
        {
            GameObject prefab = Resources.Load<GameObject>(address);
            return Object.Instantiate(prefab, at, rotation);
        }

        public GameObject Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(address);
            return Object.Instantiate(prefab, at, rotation, parent);
        }
    }
}