using MainGame.Infrastructure.Services.SceneLoader;
using MainGame.Infrastructure.StateMachine.Core;
using Zenject;

namespace MainGame.Infrastructure.StateMachine.BootstrapStates
{
    public class BootstrapState : IState
    {
        private readonly GameStatemachine _gameStatemachine;
        private readonly ILoadingCurtain _loadingCurtain;

        public BootstrapState(GameStatemachine gameStatemachine, ILoadingCurtain loadingCurtain)
        {
            _gameStatemachine = gameStatemachine;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            // warm up services
            _loadingCurtain.Show();
            _gameStatemachine.EnterLoadLevelState();
        }

        public void Exit()
        {
            
        }
        
        public class Factory : PlaceholderFactory<GameStatemachine, BootstrapState>{} 
    }
}