using Cysharp.Threading.Tasks;
using MainGame.Services.Asset;
using MainGame.Services.Camera;
using MainGame.StaticData;
using UnityEngine;
using Zenject;

namespace MainGame.Bootstrap
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

        private async UniTaskVoid Start()
        {
            GameObject player = await _injectedAssetProvider.Instantiate(PrefabAddresses.Player, Vector3.zero);
            await _cameraService.SpawnCameraRig(Vector3.zero);
            _cameraService.SetFollow(player.transform);
        }
    }
}
