using Code.Data.Configs.HeroConfigs;
using Code.Services;

namespace Code.Character.Hero.HeroInterfaces
{
    public class HeroStats : IHeroStats
    {
        private readonly IHero _hero;
        private readonly MovementLimiter _movementLimiter;
        private readonly HeroConfig _heroConfig;

        public HeroStats(IHero hero, MovementLimiter movementLimiter, HeroConfig heroConfig)
        {
            _hero = hero;
            _movementLimiter = movementLimiter;
            _heroConfig = heroConfig;
        }

        #region Conditions

        public Constants.HeroMode Mode => Constants.HeroMode.Default;
        public int Direction => _hero.Movement.DirectionX > 0 ? 1 : -1;
        public bool IsBlockMove => !_movementLimiter.CharactersCanMove;
        public bool IsDash => false;
        public bool IsMove => _hero.Movement.DirectionX != 0;
        public bool IsCrouch => _hero.Movement.IsCrouch;
        public bool IsAttack => false;
        public bool IsDeath => false;
        public bool IsJump => _hero.Jump.IsCurrentlyJumping;
        public bool IsWounded => false;
        public bool OnGround => _hero.Collision.OnGround;

        #endregion

        #region Stats

        public float CurrentHeath => 0;

        public float MaxHeath => 0;
        public float BonusHealth => 0;
        public float Damage => 0;

        public float ModeSpeedMultiplayer => 1;
        public float Speed => _heroConfig.HeroParams.MaxSpeed;
        public float JumpHeight => _heroConfig.HeroParams.JumpHeight;
        public int AirJump => 0;
        public float MelleAttackSpeed => 0;

        #endregion
    }
}