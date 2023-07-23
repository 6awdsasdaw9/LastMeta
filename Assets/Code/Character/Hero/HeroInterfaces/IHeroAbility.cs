using Code.Character.Hero.Abilities;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroAbility
    {
        public HeroDashAbility DashAbility { get; }
        public HeroHandAttackAbility HandAttackAbility { get; }
        public HeroGunAttackAbility GunAttackAbility { get; }
        public HeroAbilityLevelData AbilityLevelData { get; }
        public HeroSuperJumpAbility SuperJumpAbility { get;  }
        void Init(HeroAbilityLevelData abilityLevelData);
        void LevelUpDash();
        void LevelUpHandAttack();
        void LevelUpGunAttack();
        void LevelUpSuperJump();
    }
}