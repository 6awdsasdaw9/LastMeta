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
        private static readonly int _rangeAttack_t = Animator.StringToHash("RangeAttack");
        private static readonly int _death_t = Animator.StringToHash("Die");

        private void Update()
        {
            PlayMove(Mathf.Abs(_agent.velocity.x));
        }
        
        public void PlayDeath() => _animator.SetTrigger(_death_t);
        public void PlayMelleAttack() => _animator.SetTrigger(_melleAttack_t);
        public void PlayRangeAttack() => _animator.SetTrigger(_rangeAttack_t);
        private void PlayMove(float speed) => _animator.SetFloat(_speed_f,speed);
    }
}
