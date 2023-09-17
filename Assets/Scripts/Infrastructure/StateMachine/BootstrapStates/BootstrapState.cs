using Cysharp.Threading.Tasks;
using MainGame.Infrastructure.Services.AdsService;
using MainGame.Infrastructure.Services.LocalizationService;
using MainGame.Infrastructure.Services.SceneLoader;
using MainGame.Infrastructure.StateMachine.Core;
using MainGame.StaticData;
using Zenject;

namespace MainGame.Infrastructure.StateMachine.BootstrapStates
{
    public class BootstrapState : IState
    {
        private readonly GameStatemachine _gameStatemachine;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IAdsService _adsService;
        private readonly ILocalizationService _localizationService;
        private readonly ISceneLoadService _sceneLoadService;

        public BootstrapState(GameStatemachine gameStatemachine, 
            ILoadingCurtain loadingCurtain,
            IAdsService adsService,
            ILocalizationService localizationService,
            ISceneLoadService sceneLoadService)
        {
            _gameStatemachine = gameStatemachine;
            _loadingCurtain = loadingCurtain;
            _adsService = adsService;
            _localizationService = localizationService;
            _sceneLoadService = sceneLoadService;
        }

        public void Enter()
        {
            // warm up services
            _loadingCurtain.Show();
            _adsService.Initialize();
            _localizationService.Initialize();

            _sceneLoadService.Load(SceneNames.GameplaySceneName);
            _gameStatemachine.EnterLoadLevelState();
        }

        public void Exit()
        {
            
        }
        
        public class Factory : PlaceholderFactory<GameStatemachine, BootstrapState>{} 
    }
}