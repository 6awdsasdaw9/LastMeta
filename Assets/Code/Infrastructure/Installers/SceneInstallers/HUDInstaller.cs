using Code.Data.Configs;
using Code.UI.HeadUpDisplay;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class HUDInstaller : MonoInstaller
    {
        [SerializeField, EnumToggleButtons] private Constants.GameMode gameMode; 
        private HudSettings _hudSettings;

        [Inject]
        private void Construct(HudSettings hudSettings)
        {
            _hudSettings = hudSettings;
        }
        
        public override void InstallBindings()
        {
            BindHUD();
        }

        private void BindHUD()
        {
            HudFacade hudFacade = Container.InstantiatePrefabForComponent<HudFacade>(
                GetHudPrefabs(),
                Vector3.zero,
                Quaternion.identity,
                null);
            
            Container.Bind<HudFacade>().FromInstance(hudFacade).AsSingle().NonLazy();
        }

        private HudFacade GetHudPrefabs()
        {
            switch (gameMode)
            {
                case Constants.GameMode.Real:
                    return _hudSettings.realHudFacade;
                case Constants.GameMode.Game:
                default:
                    return _hudSettings.gameHudFacade;
            }
        }
    }
}