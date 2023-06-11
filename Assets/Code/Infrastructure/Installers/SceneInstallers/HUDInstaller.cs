using Code.Data.Configs;
using Code.PresentationModel.HeadUpDisplay;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class HUDInstaller : MonoInstaller
    {
        [SerializeField] private Constants.GameMode gameMode; 
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
            Hud hud = Container.InstantiatePrefabForComponent<Hud>(
                GetHudPrefabs(),
                Vector3.zero,
                Quaternion.identity,
                null);
            
            Container.Bind<Hud>().FromInstance(hud).AsSingle().NonLazy();
        }

        private Hud GetHudPrefabs()
        {
            switch (gameMode)
            {
                case Constants.GameMode.Real:
                    return _hudSettings.RealHUD;
                case Constants.GameMode.Game:
                default:
                    return _hudSettings.GameHUD;
            }
        }
    }
}