using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        
        private static readonly int Speed_f = Animator.StringToHash("Speed");
        private static readonly int Attack_t = Animator.StringToHash("Attack");
        private static readonly int death_t = Animator.StringToHash("Die");
        private static readonly int win_t = Animator.StringToHash("Win");

        private void Update()
        {
            PlayMove(Mathf.Abs(_agent.velocity.x));
        }

        public void PlayDeath() => _animator.SetTrigger(death_t);
        public void PlayWin() => _animator.SetTrigger(win_t);
        public void PlayAttack() => _animator.SetTrigger(Attack_t);

        private void PlayMove(float speed) => 
            _animator.SetFloat(Speed_f,speed);
    }
}
