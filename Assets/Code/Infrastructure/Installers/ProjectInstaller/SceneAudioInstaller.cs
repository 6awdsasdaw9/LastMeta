using Code.Audio;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class SceneAudioInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAudioManager();
        }

        private void BindAudioManager()
        {
            Container.Bind<SceneAudioController>().AsSingle().NonLazy();
        }
    }
}