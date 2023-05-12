using Cysharp.Threading.Tasks;
using MainGame.Infrastructure.Services.Asset.Interfaces;
using UnityEngine;
using Zenject;

namespace MainGame.Infrastructure.Services.Asset
{
    public class InjectedAssetProvider : IInjectedAssetProvider
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly IInstantiator _instantiator;

        public InjectedAssetProvider(IAssetProvider assetProvider, DiContainer diContainer, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
            _instantiator = instantiator;
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
            var go = await _assetProvider.Instantiate(address, at);
            _diContainer.InjectGameObject(go);
            return go;
        }

        public async UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation)
        {
            var go = await _assetProvider.Instantiate(address, at, rotation);
            _diContainer.InjectGameObject(go);
            return go;
        }

        public async UniTask<GameObject> Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent)
        {
            var go = await _assetProvider.Instantiate(address, at, rotation, parent);
            _diContainer.InjectGameObject(go);
            return go;
        }
    }
}