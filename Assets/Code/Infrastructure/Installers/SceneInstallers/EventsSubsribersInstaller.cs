using Code.Services;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class EventsSubsribersInstaller: MoneyInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventSubsribersStorage>().AsSingle().NonLazy();
        }
    }
}