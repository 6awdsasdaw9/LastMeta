using Code.Services.EventsSubscribes;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class EventsSubsribersStorageInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventSubsribersStorage>().AsSingle().NonLazy();
        }
    }
}