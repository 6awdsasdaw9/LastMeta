using Code.Character.Hero;
using Code.Data.Configs;
using Code.Data.GameData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class HeroInstaller : MonoInstaller
    {
        [SerializeField] private Constants.TypeOfScene _typeOfScene;
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
            switch (_typeOfScene)
            {
                case Constants.TypeOfScene.Game:
                    return _prefabsData.HeroPrefab;
                case Constants.TypeOfScene.Real:
                default:
                    return _prefabsData.RealHeroPrefab;
            }
        }

        private Vector3 GetInitialPoint() => 
            GameObject.FindGameObjectWithTag(Constants.InitialPointTag).transform.position;
    }
}