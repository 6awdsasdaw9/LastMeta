using Code.Character.Enemies.EnemiesFacades;
using Code.Logic.Collisions;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class VulnerableZone : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private CollisionObserver _collisionObserver;
        [SerializeField] private EnemyFacade _enemy;
        
        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _collisionObserver.OnEnter += OnEnter;
            }
            else
            {
                _collisionObserver.OnEnter -= OnEnter;
            }
        }

        private void OnEnter(GameObject collider)
        {
            _enemy.Health.TakeDamage(1);
        }

        private void OnValidate()
        {
            _enemy = GetComponent<EnemyFacade>();
        }
    }
}