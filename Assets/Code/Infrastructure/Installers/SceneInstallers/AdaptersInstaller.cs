using Code.Logic.Adaptors;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class AdaptersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAudioAdapter();
        }

        private void BindAudioAdapter()
        {
            Container.Bind<AudioAdapter>().AsSingle().NonLazy();
        }
    }
}