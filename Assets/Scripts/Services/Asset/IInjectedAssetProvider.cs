using UnityEngine;

namespace MainGame.Services.Asset
{
    public interface IInjectedAssetProvider
    {
        GameObject Instantiate(GameObject prefab, Transform parent = null);
        GameObject Instantiate(string address);
        GameObject Instantiate(string address, Vector3 at);
        GameObject Instantiate(string address, Vector3 at, Quaternion rotation);
        GameObject Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent);
    }
}