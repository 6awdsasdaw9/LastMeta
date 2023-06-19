using Code.Character.Hero.HeroInterfaces;
using Code.Services;

namespace Code.Character.Hero
{
    public class HeroStateListener
    {
        private readonly IHero _hero;
        private readonly MovementLimiter _movementLimiter;

        #region Conditions
        public bool IsBlockMove => !_movementLimiter.CharactersCanMove;
        public bool IsDash => _hero.Ability.HeroDashAbility.IsDash;
        public bool IsMove => _hero.Movement.DirectionX != 0;
        public bool IsCrouch => _hero.Movement.IsCrouch;
        public bool IsAttack => _hero.GunAttack.IsAttack || _hero.HandAttack.IsAttack;
        public bool IsDeath => _hero.Health.Current <= 0;
        public bool IsJump => !_hero.Collision.OnGround;

        #endregion

        #region Stats

        public float CurrentHeath => _hero.Health.Current;
        public float MaxHeath => _hero.Health.Max;
        public float BonusHealth => _hero.Upgrade.BonusHealth;

        #endregion
        
        
        public HeroStateListener(IHero hero, MovementLimiter movementLimiter)
        {
            _hero = hero;
            _movementLimiter = movementLimiter;
        }
    }
}