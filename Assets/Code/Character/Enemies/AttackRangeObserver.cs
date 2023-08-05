using Code.Logic.Collisions.Triggers;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    public class AttackRangeObserver : MonoBehaviour
    {
        [SerializeField] private EnemyMelleAttack enemyMelleAttack;
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
            
            enemyMelleAttack.DisableAttack();
        }

        private void OnDisable()
        {
            _triggerObserver.OnEnter -= OnEnter;
            _triggerObserver.OnExit -= OnExit;
            _limiter.OnDisableMovementMode -= StopCheck;
        }

        private void OnEnter(Collider obj)
        {
            enemyMelleAttack.EnableAttack();
        }

        private void OnExit(Collider obj)
        {
            enemyMelleAttack.DisableAttack();
        }

        private void StopCheck()
        {
            enemyMelleAttack.enabled = false;
            enabled = false;
        }
    }
}