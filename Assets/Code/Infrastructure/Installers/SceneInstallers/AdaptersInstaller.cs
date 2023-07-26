using Code.Services.Adapters;
using Code.Services.Adapters.HudAdapters;
using Code.UI.HeadUpDisplay.Adapters;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class AdaptersInstaller : MonoInstaller
    {
        [SerializeField, EnumToggleButtons]private Constants.GameMode _gameMode;
        
        public override void InstallBindings()
        {
            BindAudioAdapter();
            BindHudAdapters();
            BindHeroStateAdapter();
        }

        private void BindAudioAdapter()
        {
            Container.Bind<SceneAudioParamAdapter>().AsSingle().NonLazy();
        }

        private void BindHudAdapters()
        {
            Container.Bind<MenuAdapter>().AsSingle().NonLazy();
            Container.Bind<WindowsAdapter>().AsSingle().NonLazy();
            
            if(_gameMode == Constants.GameMode.Real) return;
            
            Container.BindInterfacesAndSelfTo<TimeAdapter>().AsSingle().NonLazy();;
            Container.Bind<DialogueWindowAdapter>().AsSingle().NonLazy();
            Container.Bind<HeroArtefactsPanelAdapter>().AsSingle().NonLazy();
           // Container.Bind<HeroParamPanelAdapter>().AsSingle().NonLazy();
        }

        private void BindHeroStateAdapter()
        {
            if(_gameMode == Constants.GameMode.Real) return;
            Container.Bind<HeroStateAdapter>().AsSingle().NonLazy();
        }
    }
}