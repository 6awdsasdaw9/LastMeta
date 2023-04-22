using Code.Logic.Triggers;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private TriggerObserver _triggerObserver;
        private MovementLimiter _limiter;
        
        [Inject]
        private void Coroutine(MovementLimiter limiter)
        {
            _limiter = limiter;
        }
        
        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            _limiter.OnDisableMovementMode += StopCheck;
            
            _enemyAttack.DisableAttack();
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
            _limiter.OnDisableMovementMode -= StopCheck;
        }

        private void TriggerEnter(Collider obj)
        {
            _enemyAttack.EnableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            _enemyAttack.DisableAttack();
        }

        private void StopCheck()
        {
            _enemyAttack.enabled = false;
            this.enabled = false;
        }
    }
}