namespace Code.Character.Hero
{
    public interface IHeroAbility
    {
        public HeroDashAbility HeroDashAbility { get; }
        public HeroHandAttackAbility HandAttackAbility { get; }
        public HeroGunAttackAbility GunAttackAbility { get; }
        public HeroAbilityLevelData AbilityLevelData { get; }
        void Init(HeroAbilityLevelData abilityLevelData);
        void LevelUpDash();
        void OpenDash(int level);
    }
}