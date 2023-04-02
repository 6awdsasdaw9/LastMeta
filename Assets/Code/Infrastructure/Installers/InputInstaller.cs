using Code.Services.Input;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }
        
        private void BindInput() =>
            Container.Bind<InputService>()
                .AsSingle()
                .NonLazy();
    }
}