using Code.Character.Hero;
using Code.Data.Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class HeroInstaller : MonoInstaller
    {
        [SerializeField, EnumToggleButtons] private Constants.GameMode _gameMode;
        [Inject] private AssetsConfig _assetsConfig;

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