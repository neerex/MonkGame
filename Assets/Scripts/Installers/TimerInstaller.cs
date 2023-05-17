using MainGame.Infrastructure.Services.Timer;
using Zenject;

namespace MainGame.Installers
{
    public class TimerInstaller : Installer<TimerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TimeTickProvider>().AsSingle();
            Container.BindFactory<TimerType, float, XTimer, XTimer.Factory>().AsSingle();
        }
    }
}