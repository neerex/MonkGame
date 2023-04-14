using UnityEngine;
using Zenject;

namespace MainGame.Services.Asset
{
    public class InjectedAssetProvider : IInjectedAssetProvider
    {
        private readonly IInstantiator _instantiator;

        public InjectedAssetProvider(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public GameObject Instantiate(GameObject prefab, Transform parent = null) => 
            _instantiator.InstantiatePrefab(prefab, parent);

        public GameObject Instantiate(string address) => 
            _instantiator.InstantiatePrefabResource(address);

        public GameObject Instantiate(string address, Vector3 at) => 
            _instantiator.InstantiatePrefabResource(address, at, Quaternion.identity, null);

        public GameObject Instantiate(string address, Vector3 at, Quaternion rotation) => 
            _instantiator.InstantiatePrefabResource(address, at, rotation, null);

        public GameObject Instantiate(string address, Vector3 at, Quaternion rotation, Transform parent) => 
            _instantiator.InstantiatePrefabResource(address, at, rotation, parent);
    }
}