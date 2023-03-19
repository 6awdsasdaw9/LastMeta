using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Character.Hero
{
    [RequireComponent(typeof(Animator), typeof(HeroMovement),typeof(HeroCollision))]
    public class HeroAnimator : MonoBehaviour
    {
        [Title("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private HeroMovement _hero;
        [SerializeField] private HeroCollision _collision;
        
        private readonly int Speed_f = Animator.StringToHash("Speed");
        private readonly int Jump_b = Animator.StringToHash("Jump");
        private readonly int Attack_t = Animator.StringToHash("Attack");
        private readonly int Dash_t = Animator.StringToHash("Dash");
        private readonly int Die_t = Animator.StringToHash("Death");
        private readonly int Crouch_b = Animator.StringToHash("Crouch");
        private readonly int GunMode_b = Animator.StringToHash("Gun");
        
        private void Update()
        {
            PlayMove();
            PlayCrouch();
            PlayJump();
        }

        private void PlayJump() => 
            _animator.SetBool(Jump_b, !_collision.onGround);

        private void PlayMove() => 
            _animator.SetFloat(Speed_f, Mathf.Abs(_hero.directionX));

        private void PlayCrouch() => 
            _animator.SetBool(Crouch_b, _hero.isCrouch);

        public void PlayAttack() => 
            _animator.SetTrigger(Attack_t);

        public void PlayDeath() => 
            _animator.SetTrigger(Die_t);
        
    }
}