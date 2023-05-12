using MainGame.Infrastructure.StateMachine.Core;
using Zenject;

namespace MainGame.Infrastructure.StateMachine.BootstrapStates
{
    public class BootstrapState : IState
    {
        private readonly GameStatemachine _gameStatemachine;

        public BootstrapState(GameStatemachine gameStatemachine)
        {
            _gameStatemachine = gameStatemachine;
        }

        public void Enter()
        {
            // warm up services
            
            _gameStatemachine.EnterLoadLevelState();
        }

        public void Exit()
        {
            
        }
        
        public class Factory : PlaceholderFactory<GameStatemachine, BootstrapState>{}
    }
}