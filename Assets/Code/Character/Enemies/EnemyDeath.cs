using System;
using System.Collections;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyHealth),typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth health;
        public EnemyAnimator animator;
        public EnemyAttack enemyAttack;
        public AgentMoveToHero agent;

        public GameObject deathFX;

        public event Action Happened;

        private void Start()
        {
            health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            health.HealthChanged -= OnHealthChanged;
            
            animator.PlayDeath();
            
            agent.enabled = false;
            enemyAttack.enabled = false;
            
            
            SpawnDeathFx();
            
            StartCoroutine(DestroyTimer());
            
            Happened?.Invoke();
        }

        private void SpawnDeathFx()
        {
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}