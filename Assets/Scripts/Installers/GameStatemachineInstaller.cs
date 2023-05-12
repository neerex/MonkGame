using MainGame.Infrastructure.StateMachine.BootstrapStates;
using Zenject;

namespace MainGame.Installers
{
    public class GameStatemachineInstaller : Installer<GameStatemachineInstaller>
    {
        public override void InstallBindings() 
        {
            BindGameStateMachine();
            BindGameStates();
        }
        
        private void BindGameStates()
        {
            Container.BindFactory<GameStatemachine, BootstrapState, BootstrapState.Factory>().AsSingle();
            Container.BindFactory<GameStatemachine, LoadLevelState, LoadLevelState.Factory>().AsSingle();
            Container.BindFactory<GameStatemachine, GameLoopState,  GameLoopState.Factory>().AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStatemachine>().To<GameStatemachine>().AsSingle();
        }
    }
}