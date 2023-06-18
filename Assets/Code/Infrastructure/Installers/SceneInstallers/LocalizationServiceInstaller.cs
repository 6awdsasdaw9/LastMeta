using Code.Logic.LanguageLocalization;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class LocalizationServiceInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLocalizationController();
            BindHudLocalization();
        }

        private void BindLocalizationController()
        {
            Container.Bind<LocalizationController>().AsSingle().NonLazy();
        }

        private void BindHudLocalization()
        {
            Container.Bind<HudLocalization>().AsSingle().NonLazy();
        }
    }
}