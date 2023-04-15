using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MainGame.Services.Asset
{
    public class InjectedAssetProvider : IInjectedAssetProvider
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;

        public InjectedAssetProvider(IAssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }

        public GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            return _instantiator.InstantiatePrefab(prefab, parent);
        }

        public async UniTask<GameObject> Instantiate(string address)
        {
            var go = await _assetProvider.Instantiate(address);
            _diContainer.InjectGameObject(go);
            return go;
        }

        public async UniTask<GameObject> Instantiate(string address, Vector3 at)
        {
            var go = await _assetProvider.Instantiate(address);
            _diContainer.InjectGameObject(go);
            return go;
        }

        public async UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation)
        {
            var go = await _assetProvider.Instantiate(address);
            _diContainer.InjectGameObject(go);
            return go;
        }

        public async UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent)
        {
            var go = await _assetProvider.Instantiate(address);
            _diContainer.InjectGameObject(go);
            return go;
        }
    }
}