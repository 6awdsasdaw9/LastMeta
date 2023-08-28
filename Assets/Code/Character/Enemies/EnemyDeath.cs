using Code.Character.Enemies.EnemiesFacades;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemyDeath : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private EnemyFacade _facade;

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _facade.Health.OnHealthChanged += OnHealthChanged;
            }
            else
            {
                _facade.Health.OnHealthChanged -= OnHealthChanged;
            }
        }
        
        private void OnHealthChanged()
        {
            if (_facade.Health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _facade.Health.OnHealthChanged -= OnHealthChanged;
            _facade.Die();
        }

        private void OnValidate()
        {
            _facade = GetComponent<EnemyFacade>();
        }
    }
}