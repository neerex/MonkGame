using MainGame.Infrastructure.StateMachine.Core;
using Zenject;

namespace MainGame.Infrastructure.StateMachine.BootstrapStates
{
    public class GameLoopState : IState
    {
        private readonly GameStatemachine _gameStatemachine;

        public GameLoopState(GameStatemachine gameStatemachine)
        {
            _gameStatemachine = gameStatemachine;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public class Factory : PlaceholderFactory<GameStatemachine, GameLoopState>{}
    }
}