using Code.Logic.Adaptors;
using Code.Services;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class MovementLimiterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMovementLimiter();
            BindMovementLimiterAdapter();
        }

        private void BindMovementLimiter() =>
            Container.Bind<MovementLimiter>().AsSingle().NonLazy();

        private void BindMovementLimiterAdapter() => 
            Container.Bind<MovementLimiterAdapter>().AsSingle().NonLazy();
    }
}