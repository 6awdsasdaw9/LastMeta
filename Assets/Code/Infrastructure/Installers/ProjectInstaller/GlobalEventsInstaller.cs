using Code.Infrastructure.GlobalEvents;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class GlobalEventsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEventsFacade();
        }

        private void BindEventsFacade()
        {
            Container.Bind<EventsFacade>().AsSingle().NonLazy();
        }
    }
}