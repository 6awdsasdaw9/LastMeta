using Code.Character.Enemies.EnemiesFacades;
using Code.Logic.Collisions.Triggers;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class VulnerableZone : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private EnemyFacade _enemy;

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

        private void OnEnter(Collider collider)
        {
            _enemy.Health.TakeDamage(1);
        }

        private void OnValidate()
        {
            _enemy = GetComponent<EnemyFacade>();
        }
    }
}