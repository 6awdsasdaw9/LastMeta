using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        
        private static readonly int _speed_f = Animator.StringToHash("Speed");
        private static readonly int _melleAttack_t = Animator.StringToHash("MelleAttack");
        private static readonly int _melleAttackSpeed_f = Animator.StringToHash("MelleAttackSpeed");
        private static readonly int _rangeAttack_t = Animator.StringToHash("RangeAttack");
        private static readonly int _rangeAttackSpeed_f = Animator.StringToHash("RangeAttackSpeed");
        private static readonly int _death_t = Animator.StringToHash("Die");

        private void Update()
        {
            PlayMove(Mathf.Abs(_agent.velocity.x));
        }
        
        public void PlayDeath() => _animator.SetTrigger(_death_t);
        public void PlayMelleAttack() => _animator.SetTrigger(_melleAttack_t);
        public void SetMelleAttackAnimationSpeed(float value) => _animator.SetFloat(_melleAttackSpeed_f, value); 
        public void PlayRangeAttack() => _animator.SetTrigger(_rangeAttack_t);
        public void SetRangeAttackAnimationSpeed(float value) => _animator.SetFloat(_rangeAttackSpeed_f, value); 
        private void PlayMove(float speed) => _animator.SetFloat(_speed_f,speed);

        private void OnValidate()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
        }
    }
}
