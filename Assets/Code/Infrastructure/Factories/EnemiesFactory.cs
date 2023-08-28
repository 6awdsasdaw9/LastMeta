using System.Collections.Generic;
using System.Linq;
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
        private readonly List<EnemyPool> _enemiesPool = new();

        public EnemiesFactory(DiContainer container, EnemiesConfig config)
        {
            _container = container;
            _config = config;
        }

        public EnemyFacade GetEnemy(EnemyType type, Vector3 postion)
        {
            var prefab = _config.GetFacadeByType(type);
            EnemyFacade enemy;
            
            if (GetPool(type, out var pool) && pool.GetFreeEnemy(out enemy))
            {
                return enemy;
            }

            enemy = Object.Instantiate(prefab, postion, Quaternion.identity);
            enemy.Construct(_container);
            pool.AddNewEnemy(enemy);
            return enemy;
        }


        private bool GetPool(EnemyType type, out EnemyPool pool)
        {
            pool = _enemiesPool.FirstOrDefault(p => p.Type == type);
            if (pool == null)
            {
                pool = new EnemyPool() { Type = type };
                _enemiesPool.Add(pool);
            }

            return pool is { IsBusy: false };
        }
    }


    public class EnemyPool
    {
        public EnemyType Type;
        private readonly List<EnemyFacade> _activeEnemies = new();
        private readonly List<EnemyFacade> _allEnemies = new();
        public bool IsBusy => _activeEnemies.Count == _allEnemies.Count;

        public void AddNewEnemy(EnemyFacade enemy)
        {
            if (!_allEnemies.Contains(enemy))
            {
                _allEnemies.Add(enemy);
            }

            if (!_activeEnemies.Contains(enemy))
            {
                _activeEnemies.Add(enemy);
            }
        }

        public bool GetFreeEnemy(out EnemyFacade enemy)
        {
            var freeEnemies = _allEnemies.Except(_activeEnemies).ToList();
            if (freeEnemies.Count > 0)
            {
                enemy = freeEnemies[0];
                return true;
            }

            enemy = null;
            return false;
        }
    }
}