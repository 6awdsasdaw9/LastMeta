using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    public class CheckAttackRange : MonoBehaviour
    {
        public EnemyAttack enemyAttack;
        public TriggerObserver triggerObserver;
        [Inject] private MovementLimiter _limiter;

        
        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;
            _limiter.OnDisableMovementMode += Disable;
            enemyAttack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            enemyAttack.EnableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            enemyAttack.DisableAttack();
        }

        private void Disable()
        {
            triggerObserver.TriggerEnter -= TriggerEnter;
            triggerObserver.TriggerExit -= TriggerExit;
            _limiter.OnDisableMovementMode -= Disable;
            enemyAttack.enabled = false;
            enabled = false;
            //enabled = false;
        }
    }
}