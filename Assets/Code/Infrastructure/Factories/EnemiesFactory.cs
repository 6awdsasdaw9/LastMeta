using Code.Character.Enemies.EnemiesFacades;
using Code.Data.Configs;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class EnemiesFactory
    {
        private readonly DiContainer _container;
        private readonly EnemiesConfig _config;

        public EnemiesFactory(DiContainer container, EnemiesConfig config)
        {
            _container = container;
            _config = config;
        }

        public EnemyFacade InstantiateEnemy(EnemyType type, Vector3 postion)
        {
            var prefab = _config.GetFacadeByType(type);
            var newEnemy = GameObject.Instantiate(prefab, postion, Quaternion.identity);

            newEnemy.Construct(_container);
          
            
            return newEnemy;
        }
    }
}