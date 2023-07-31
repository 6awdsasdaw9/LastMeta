using Code.Logic.Triggers;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    public class CheckMelleAttackRange : MonoBehaviour
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
            _triggerObserver.OnEnter += OnEnter;
            _triggerObserver.OnExit += OnExit;
            _limiter.OnDisableMovementMode += StopCheck;
            
            _enemyAttack.DisableAttack();
        }

        private void OnDisable()
        {
            _triggerObserver.OnEnter -= OnEnter;
            _triggerObserver.OnExit -= OnExit;
            _limiter.OnDisableMovementMode -= StopCheck;
        }

        private void OnEnter(Collider obj)
        {
            _enemyAttack.EnableAttack();
        }

        private void OnExit(Collider obj)
        {
            _enemyAttack.DisableAttack();
        }

        private void StopCheck()
        {
            _enemyAttack.enabled = false;
            enabled = false;
        }
    }
}