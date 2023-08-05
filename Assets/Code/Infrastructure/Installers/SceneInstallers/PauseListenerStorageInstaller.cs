using Code.Services.PauseListeners;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class PauseListenerStorageInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PauseListenerStorage>().AsSingle().NonLazy();
        }
    }
}