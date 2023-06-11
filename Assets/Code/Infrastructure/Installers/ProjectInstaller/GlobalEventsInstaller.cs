using Code.Infrastructure.GlobalEvents;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class GlobalEventsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameEvents();
        }

        private void BindGameEvents()
        {
            Container.Bind<GameEvents>().AsSingle().NonLazy();
        }
    }
}