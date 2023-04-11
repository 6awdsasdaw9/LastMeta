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
            HeroMovement hero = Container.InstantiatePrefabForComponent<HeroMovement>(
                GetHeroPrefabs(),
                GetInitialPoint(),
                Quaternion.identity,
                null);

            Container.Bind<HeroMovement>().FromInstance(hero).AsSingle().NonLazy();
        }

        private HeroMovement GetHeroPrefabs()
        {
            switch (_typeOfScene)
            {
                case Constants.TypeOfScene.Game:
                    return _prefabsData.hero;
                case Constants.TypeOfScene.Real:
                default:
                    return _prefabsData.realHero;
            }
        }

        private Vector3 GetInitialPoint() => 
            GameObject.FindGameObjectWithTag(Constants.InitialPointTag).transform.position;
    }
}