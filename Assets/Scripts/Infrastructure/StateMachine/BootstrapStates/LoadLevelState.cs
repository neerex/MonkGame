using Cysharp.Threading.Tasks;
using MainGame.CharacterResources.Interfaces;
using MainGame.Entities.Player;
using MainGame.Infrastructure.Services.Camera.Interfaces;
using MainGame.Infrastructure.Services.EntitiesProviders.Player.Interfaces;
using MainGame.Infrastructure.Services.SceneLoader;
using MainGame.Infrastructure.StateMachine.Core;
using MainGame.Stats.Interfaces;
using MainGame.UI;
using UnityEngine;
using Zenject;

namespace MainGame.Infrastructure.StateMachine.BootstrapStates
{
    public class LoadLevelState : IState 
    {
        private readonly GameStatemachine _gameStatemachine;
        private readonly IPlayerProvider _playerProvider;
        private readonly ICameraService _cameraService;
        private readonly ILoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStatemachine gameStatemachine, IPlayerProvider playerProvider, ICameraService cameraService, ILoadingCurtain loadingCurtain)
        {
            _gameStatemachine = gameStatemachine;
            _playerProvider = playerProvider;
            _cameraService = cameraService;
            _loadingCurtain = loadingCurtain;
        }

        public async void Enter()
        {
            await _cameraService.SpawnCameraRig(Vector3.zero);
            Player player = await InitializePlayer();
            _cameraService.SetFollow(player.transform);
            
            _loadingCurtain.Hide();
            _gameStatemachine.EnterGameLoopState();
        }

        public void Exit()
        {
            
        }
        
        private async UniTask<Player> InitializePlayer()
        {
            Player player = await _playerProvider.SpawnPlayer(Vector3.zero);
            player.GetComponent<IStatHolder>().InitializeStatLibrary();
            foreach (IStatsReader statReader in player.GetComponentsInChildren<IStatsReader>())
            {
                statReader.InitializeStats();
            }

            var playerHealth = player.GetComponent<IHealth>();
            player.GetComponentInChildren<ActorUI>().Construct(playerHealth);
            
            return player;
        }
        
        public class Factory : PlaceholderFactory<GameStatemachine, LoadLevelState>{}
    }
}