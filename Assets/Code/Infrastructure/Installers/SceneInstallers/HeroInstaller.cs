using Code.Character.Hero;
using Code.Data.Configs;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class HeroInstaller : MonoInstaller
    {
        [SerializeField] private Constants.GameMode _gameMode;
        [Inject] private PrefabsData _prefabsData;

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
                    return _prefabsData.HeroPrefab;
                case Constants.GameMode.Real:
                default:
                    return _prefabsData.RealHeroPrefab;
            }
        }

        private Vector3 GetInitialPoint() => 
            GameObject.FindGameObjectWithTag(Constants.InitialPointTag).transform.position;
    }
}