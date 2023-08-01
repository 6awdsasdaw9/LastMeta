using System;
using System.Collections;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyHealth),typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyMelleAttack enemyMelleAttack;
        [SerializeField] private EnemyMovementPatrol _agent;
        
        public event Action Happened;

        private void Start()
        {
            _health.OnHealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _health.OnHealthChanged -= OnHealthChanged;
            
            _animator.PlayDeath();
            
            _agent.enabled = false;
            enemyMelleAttack.enabled = false;
            _collider.enabled = false;
            
            StartCoroutine(DestroyCoroutine());
            
            Happened?.Invoke();
        }



        private IEnumerator DestroyCoroutine()
        {
            _collider.enabled = false;       
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}