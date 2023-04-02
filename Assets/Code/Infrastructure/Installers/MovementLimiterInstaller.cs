using Code.Services;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class MovementLimiterInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            BindMovementLimiter();

        private void BindMovementLimiter() =>
            Container.Bind<MovementLimiter>()
                .AsSingle()
                .NonLazy();

    }
}