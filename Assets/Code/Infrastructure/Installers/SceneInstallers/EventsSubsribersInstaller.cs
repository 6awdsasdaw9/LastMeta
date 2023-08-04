using Code.Services;
using Code.Services.EventsSubscribes;

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