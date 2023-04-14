using MainGame.Services.Asset;
using MainGame.Services.Camera;
using MainGame.StaticData;
using UnityEngine;
using Zenject;

namespace MainGame
{
    public class Bootstrap : MonoBehaviour
    {
        private IInjectedAssetProvider _injectedAssetProvider;
        private ICameraService _cameraService;

        [Inject]
        public void Construct(
            IInjectedAssetProvider injectedAssetProvider, 
            ICameraService cameraService)
        {
            _injectedAssetProvider = injectedAssetProvider;
            _cameraService = cameraService;
        }

        private void Start()
        {
            var player = _injectedAssetProvider.Instantiate(PrefabAddresses.Player, Vector3.zero);
            _cameraService.SpawnCameraRig(Vector3.zero);
            _cameraService.SetFollow(player.transform);
        }
    }
}
