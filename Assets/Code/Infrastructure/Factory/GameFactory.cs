using Code.Character.Hero;
using Code.Data.GameData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory
{
    
    public class GameFactory /* : IGameFactory*/
    {
        private readonly DiContainer _diContainer;
        private readonly PrefabsData _prefabsData;

        public GameFactory(DiContainer container, PrefabsData prefabsData)
        {
            _diContainer = container;
            _prefabsData = prefabsData;
        }
        
        /*
        public GameObject CreateHero(GameObject at)
        {
            var hero = Object.Instantiate(_prefabsData.hero, at.transform.position,Quaternion.identity);
            _diContainer.Inject(hero.GetComponent<HeroMovement>());
            return hero.gameObject;
        }
        
        public void CreateHeroT(GameObject at)
        {
            HeroMovement hero = _diContainer.InstantiatePrefabForComponent<HeroMovement>(_prefabsData.hero, at.transform.position,
                Quaternion.identity, null);
            _diContainer.Bind<HeroMovement>().FromInstance(hero).AsSingle().NonLazy();
        }

        public GameObject CreateHud()
        {
            return Object.Instantiate(_prefabsData.hud.gameObject);
        }*/
        
    }
}