using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs.HeroConfigs;
using Code.Services;

namespace Code.Character.Hero
{
    public class HeroStatsGame : IHeroStats
    {
        private readonly IHero _hero;
        private readonly MovementLimiter _movementLimiter;
        private readonly HeroConfig _heroConfig;

        public HeroStatsGame(IHero hero, MovementLimiter movementLimiter, HeroConfig heroConfig)
        {
            _hero = hero;
            _movementLimiter = movementLimiter;
            _heroConfig = heroConfig;
        }

        #region Conditions

        public Constants.HeroMode Mode => _hero.ModeToggle.Mode;
        public int Direction => _hero.Movement.DirectionX > 0 ? 1 : -1;
        public bool IsBlockMove => !_movementLimiter.CharactersCanMove;
        public bool IsDash => _hero.Ability.DashAbility is { IsDash: true };
        public bool IsMove => _hero.Movement.DirectionX != 0;
        public bool IsCrouch => _hero.Movement.IsCrouch;
        public bool IsAttack => _hero.GunAttack.IsAttack || _hero.HandAttack.IsAttack;
        public bool IsDeath => _hero.Health.Current <= 0;
        public bool IsJump => _hero.Jump.IsCurrentlyJumping;
        public bool IsWounded => _hero.Health.Current < _hero.Health.Max / 100 * 30;

        public bool OnGround => _hero.Collision.OnGround;

        #endregion

        #region Stats

        public float CurrentHeath => _hero.Health.Current;

        public float MaxHeath => _hero.Health.Max;
        public float BonusHealth => _hero.Upgrade.BonusHealth;

        public float Damage
        {
            get
            {
                switch (Mode)
                {
                    default:
                    case Constants.HeroMode.Default:
                        return _hero.Upgrade.BonusAttack +
                               (_hero.Ability.HandAttackAbility is { CurrentData: { } }
                                   ? _hero.Ability.HandAttackAbility.CurrentData.attackData.Damage
                                   : 0);
                    case Constants.HeroMode.Gun:
                        return _hero.Upgrade.BonusAttack +
                               (_hero.Ability.GunAttackAbility is { ShootingParams: { } }
                                   ? _hero.Ability.GunAttackAbility.ShootingParams.attackData.Damage
                                   : 0);
                    case Constants.HeroMode.Black:
                        return 420;
                }
            }
        }

        public float ModeSpeedMultiplayer
        {
            get
            {
                switch (Mode)
                {
                    default:
                    case Constants.HeroMode.Default:
                    case Constants.HeroMode.Black:
                        return IsDash ? _hero.Ability.DashAbility.Param.SpeedBonus + 1 : 1;
                    case Constants.HeroMode.Gun:
                        return IsDash ? _hero.Ability.DashAbility.Param.SpeedBonus + 0.6f : 0.6f;
                }
            }
        }

        public float SerfaceSpeedMultiplayer => _hero.Collision.OnMovementPlatform ? 4 : 1;

        public float Speed => _heroConfig.HeroParams.MaxSpeed + _hero.Upgrade.BonusSpeed;

        public float JumpHeight => _heroConfig.HeroParams.JumpHeight
                                   + _hero.Upgrade.BonusHeightJump
                                   + (_hero.Ability.SuperJumpAbility != null
                                       ? _hero.Ability.SuperJumpAbility.CurrentData.BonusHeightJump
                                       : 0);

        public int AirJump => _hero.Ability.SuperJumpAbility is { CurrentData: { } }
            ? _hero.Ability.SuperJumpAbility.CurrentData.MaxAirJump
            : 0;

        public float MelleAttackSpeed => _hero.Ability.HandAttackAbility is { CurrentData: { } }
            ? _hero.Ability.HandAttackAbility.CurrentData.attackData.AnimationSpeed
            : 1;

        #endregion
    }
}