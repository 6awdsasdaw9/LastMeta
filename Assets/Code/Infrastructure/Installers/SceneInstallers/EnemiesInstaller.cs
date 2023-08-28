using Code.Infrastructure.Factories;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class EnemiesInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEnemiesFactory();
        }

        private void BindEnemiesFactory()
        {
            Container.Bind<EnemiesFactory>().AsSingle().NonLazy();
        }
    }
}