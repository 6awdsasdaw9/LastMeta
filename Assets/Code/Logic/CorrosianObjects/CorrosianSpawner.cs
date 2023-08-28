using System;
using Code.Data.Configs;
using Code.Debugers;
using Code.Infrastructure.Factories;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Objects.Animations;
using Code.Services.EventsSubscribes;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Character.Enemies
{
    public class CorrosianSpawner : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private StartAnimation _startAnimation;
        [SerializeField] private EnemyType[] _spawnEnemyTypes;
        
        private EnemiesFactory _enemiesFactory;

        [Inject]
        private void Contruct(EnemiesFactory enemiesFactory)
        {
            _enemiesFactory = enemiesFactory;
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _triggerObserver.OnEnter += OnEnter;
            }
            else
            {
                
                _triggerObserver.OnEnter -= OnEnter;
            }
        }

        private void OnEnter(GameObject obj)
        {
            var random = Random.Range(0, 4);
            Logg.ColorLog($"Toy: random = {random}");
            if (random == 0)
            {
                _startAnimation.PlayStart(() => Spawn()); 
                Logg.ColorLog($"Toy: spawn");
            }
            
        }

        private void Spawn()
        {
            var spawnEnemyType = _spawnEnemyTypes[Random.Range(0, _spawnEnemyTypes.Length)];
            var enemy = _enemiesFactory.InstantiateEnemy(spawnEnemyType, transform.position);
            enemy.Revival();
            gameObject.SetActive(false);
        }
    }
    
}