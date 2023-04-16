using MainGame.Services.Asset;
using MainGame.Services.Camera;
using MainGame.Services.Input;
using MainGame.Services.Raycast;
using Zenject;

namespace MainGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //no dependencies
            BindAssetProvider();
            BindInjectedAssetProvider();
            BindPlayerInputService();
            
            //multiple dependencies
            BindCameraService();
            BindMouseRaycastService();
        }

        private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindInjectedAssetProvider()
        {
            Container.Bind<IInjectedAssetProvider>().To<InjectedAssetProvider>().AsSingle();
        }

        private void BindPlayerInputService()
        {
            Container.Bind<IPlayerInputService>().To<PlayerInputService>().AsSingle();
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
