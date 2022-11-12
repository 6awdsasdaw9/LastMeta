using UnityEngine;

namespace Code.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int death_t = Animator.StringToHash("Death");
        private static readonly int win_t = Animator.StringToHash("Win");
        private static readonly int speed_f = Animator.StringToHash("Speed");
        private static readonly int isMoving_b = Animator.StringToHash("IsMoving");
        private static readonly int attack_t = Animator.StringToHash("Attack");
    
        void Awake() => _animator = GetComponent<Animator>();

        public void PlayDeath() => _animator.SetTrigger(death_t);
        public void PlayWin() => _animator.SetTrigger(win_t);
        public void PlayAttack() => _animator.SetTrigger(attack_t);

        public void Move(float speed)
        {
            _animator.SetBool(isMoving_b,true);
            _animator.SetFloat(speed_f,speed);
        }
    
        public void StopMove() =>  _animator.SetBool(isMoving_b,false);

    }
}
