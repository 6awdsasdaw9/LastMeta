using System;
using System.Collections;
using Code.Character.Enemies.EnemiesFacades;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private EnemyFacade _facade;
        public event Action Happened;

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
            Happened?.Invoke();
        }

        private void OnValidate()
        {
            _facade = GetComponent<EnemyFacade>();
        }
    }
}