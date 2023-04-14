using UnityEngine;

namespace MainGame.Services.Asset
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string address);
        GameObject Instantiate(string address, Vector3 at);
        GameObject Instantiate(string address, Vector3 at, Quaternion rotation);
        GameObject Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent);
    }
}