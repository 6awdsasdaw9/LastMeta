using Code.Services.CurrencyServices;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class MoneyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMoneyStorage();
        }
        
        private void BindMoneyStorage()
        {
            Container.Bind<MoneyStorage>().AsSingle().NonLazy();
        }

    }
}