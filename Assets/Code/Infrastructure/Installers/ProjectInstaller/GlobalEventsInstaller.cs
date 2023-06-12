using Code.Infrastructure.GlobalEvents;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class GlobalEventsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneEvents();
            BindTimeEvents();
        }

        private void BindSceneEvents()
        {
            Container.Bind<SceneEvents>().AsSingle().NonLazy();
        }
        
        private void BindTimeEvents()
        {
            Container.Bind<TimeEvents>().AsSingle().NonLazy();
        }
    }
}