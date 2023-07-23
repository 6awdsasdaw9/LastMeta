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
        }

        private void BindAudioAdapter()
        {
            Container.Bind<SceneAudioAdapter>().AsSingle().NonLazy();
        }

        private void BindHudAdapters()
        {
            Container.Bind<MenuAdapter>().AsSingle().NonLazy();
            Container.Bind<WindowsAdapter>().AsSingle().NonLazy();
            
            if(_gameMode == Constants.GameMode.Real) return;
            
            Container.BindInterfacesAndSelfTo<TimeAdapter>().AsSingle().NonLazy();;
            Container.Bind<DialogueWindowAdapter>().AsSingle().NonLazy();
            Container.Bind<HeroPanelAdapter>().AsSingle().NonLazy();
        }
    }
}