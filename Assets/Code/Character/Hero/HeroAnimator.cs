using Code.Character.Hero.HeroInterfaces;
using UnityEngine;

namespace Code.Character.Hero
{
    public class HeroAnimator : MonoBehaviour, IHeroAnimator
    {
        [SerializeField] private Animator _animator;
        private IHero _hero;
        
        private readonly int Speed_f = Animator.StringToHash("Speed");
        private readonly int Jump_b = Animator.StringToHash("Jump");
        private readonly int Attack_t = Animator.StringToHash("Attack");
        private readonly int StopAttack_t = Animator.StringToHash("StopAttack");
        private readonly int Dash_b = Animator.StringToHash("Dash");
        private readonly int Die_t = Animator.StringToHash("Death");
        private readonly int WaterDie_t = Animator.StringToHash("DeathOnWater");
        private readonly int DamageFromAbove_t = Animator.StringToHash("DamageFromAbove");
        private readonly int Crouch_b = Animator.StringToHash("Crouch");
        private readonly int Stunned_b = Animator.StringToHash("Stunned");
        private readonly int GunMode_b = Animator.StringToHash("Gun");
        private void Awake() => _hero = GetComponent<IHero>();
        private void Update()
        {
            PlayMove();
            PlayCrouch();
            PlayJump();
        }

        public bool IsCalPlayAnimation => _animator != null && _animator.isActiveAndEnabled;

        public void PlayJump() => 
            _animator.SetBool(Jump_b, !_hero.Collision.OnGround);

        public void PlayMove() => 
            _animator.SetFloat(Speed_f, Mathf.Abs(_hero.Movement.DirectionX));

        public void PlayCrouch() => 
            _animator.SetBool(Crouch_b, _hero.Movement.IsCrouch);
        public void PlayAttack() => 
            _animator.SetTrigger(Attack_t);
        public void PlayStopAttack() => 
            _animator.SetTrigger(StopAttack_t);
        public void PlayDeath() => 
            _animator.SetTrigger(Die_t);
        public void PlayDeathOnWater() => 
            _animator.SetTrigger(WaterDie_t);
        public void PlayDash(bool isDash) => 
            _animator.SetBool(Dash_b,isDash);
        public void PlayDamageFromAbove() =>
            _animator.SetTrigger(DamageFromAbove_t);
        public void PlayStunned(bool isStunned) =>
            _animator.SetBool(Stunned_b,isStunned);
        public void PlayEnterGunMode() => 
            _animator.SetBool(GunMode_b, true);
        public void PlayEnterHandMode() => 
            _animator.SetBool(GunMode_b, false);
    }
}