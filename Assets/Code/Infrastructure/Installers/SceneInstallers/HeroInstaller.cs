using Code.Character.Hero;
using Code.Data.Configs;
using Code.Infrastructure.GlobalEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class HeroInstaller : MonoInstaller
    {
        [SerializeField, EnumToggleButtons] private Constants.GameMode _gameMode;
        private AssetsConfig _assetsConfig;
        private EventsFacade _eventsFacade;

        [Inject]
        private void Construct(AssetsConfig assetsConfig, EventsFacade eventsFacade)
        {
            _assetsConfig = assetsConfig;
            _eventsFacade = eventsFacade;
        }

        public override void InstallBindings()
        {
            BindHero();
        }
        
        private void BindHero()
        {
            Hero hero = Container.InstantiatePrefabForComponent<Hero>(
                GetHeroPrefabs(),
                GetInitialPoint(),
                Quaternion.identity,
                null);
            
            Container.BindInterfacesTo<Hero>().FromInstance(hero).AsSingle().NonLazy();
            _eventsFacade?.SceneEvents.InitHeroEvent(hero);
        }

        private Hero GetHeroPrefabs()
        {
            switch (_gameMode)
            {
                case Constants.GameMode.Game:
                    return _assetsConfig.HeroPrefab;
                case Constants.GameMode.Real:
                default:
                    return _assetsConfig.RealHeroPrefab;
            }
        }

        private Vector3 GetInitialPoint() => 
             GameObject.FindGameObjectWithTag(Constants.InitialPointTag).transform.position;
    }
}