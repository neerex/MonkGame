using MainGame.Infrastructure.Services.Asset;
using MainGame.Infrastructure.Services.Asset.Interfaces;
using MainGame.Infrastructure.Services.AsyncRoutine;
using MainGame.Infrastructure.Services.AsyncRoutine.Interfaces;
using MainGame.Infrastructure.Services.Camera;
using MainGame.Infrastructure.Services.Camera.Interfaces;
using MainGame.Infrastructure.Services.EntitiesProviders.Player;
using MainGame.Infrastructure.Services.EntitiesProviders.Player.Interfaces;
using MainGame.Infrastructure.Services.Input;
using MainGame.Infrastructure.Services.Input.Interfaces;
using MainGame.Infrastructure.Services.Raycast;
using MainGame.Infrastructure.Services.Raycast.Interfaces;
using MainGame.Infrastructure.StateMachine.BootstrapStates;
using Zenject; 

namespace MainGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTimerService();
            BindAssetProviders();
            BindPlayerInputService();
            BindCoroutineRunner();
            BindPlayerProvider();

            BindGameStatemachine();

            BindCameraService();
            BindMouseRaycastService();
        }

        private void BindTimerService()
        {
            TimerInstaller.Install(Container);
        }

        private void BindAssetProviders()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IInjectedAssetProvider>().To<InjectedAssetProvider>().AsSingle();
        }

        private void BindPlayerInputService()
        {
            Container.Bind<IPlayerInputService>().To<PlayerInputService>().AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container.Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName("[COROUTINE RUNNER]")
                .AsSingle()
                .NonLazy(); 
        }

        private void BindPlayerProvider()
        {
            Container.Bind<IPlayerProvider>().To<PlayerProvider>().AsSingle();
        }

        private void BindGameStatemachine()
        {
            Container.Bind<GameStatemachine>()
                .FromSubContainerResolve()
                .ByInstaller<GameStatemachineInstaller>()
                .AsSingle();
        }

        private void BindMouseRaycastService()
        {
            Container.Bind<IMouseRaycastService>().To<MouseRaycastService>().AsSingle();
        }

        private void BindCameraService()
        {
            Container.Bind<ICameraService>().To<CameraService>().AsSingle();
        }
    }
}
