using System.Collections.Generic;
using MainGame.Infrastructure.StateMachine.Core;

namespace MainGame.Infrastructure.StateMachine.BootstrapStates
{
    public class GameStatemachine : StateMachineBase
    {
        public GameStatemachine
        (
            BootstrapState.Factory bootstrapStateFactory,
            LoadLevelState.Factory loadLevelStateFactory,
            GameLoopState.Factory gameLoopStateFactory
        )
        {
            RegisterState(bootstrapStateFactory.Create(this));
            RegisterState(loadLevelStateFactory.Create(this));
            RegisterState(gameLoopStateFactory.Create(this));
        }

        public void EnterBootStrapState() => Enter<BootstrapState>();
        public void EnterLoadLevelState() => Enter<LoadLevelState>();
        public void EnterGameLoopState() => Enter<GameLoopState>();
    }
}