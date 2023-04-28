using Cysharp.Threading.Tasks;
using MainGame.CharacterResources.Interfaces;
using MainGame.Services.Asset.Interfaces;
using MainGame.Services.Camera.Interfaces;
using MainGame.StaticData;
using MainGame.Stats.Interfaces;
using MainGame.UI;
using UnityEngine;
using Zenject;

namespace MainGame.Services.Bootstrap
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
            // remove this into bootstrap statemachine
            
            await _cameraService.SpawnCameraRig(Vector3.zero);
            GameObject player = await InitializePlayer();
            _cameraService.SetFollow(player.transform);
        }

        // temporary here
        private async UniTask<GameObject> InitializePlayer()
        {
            GameObject player = await _injectedAssetProvider.Instantiate(PrefabAddresses.Player, Vector3.zero);
            player.GetComponent<IStatHolder>().InitializeStatLibrary();
            foreach (IStatsReader statReader in player.GetComponentsInChildren<IStatsReader>())
            {
                statReader.InitializeStats();
            }

            var playerHealth = player.GetComponent<IHealth>();
            player.GetComponentInChildren<ActorUI>().Construct(playerHealth);
            
            return player;
        }
    }
}
