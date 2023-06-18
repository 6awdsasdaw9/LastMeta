namespace Code.Character.Hero
{
    public interface IHeroAbility
    {
        public HeroAbilityLevelData AbilityLevelData { get; }
        void Init(HeroAbilityLevelData abilityLevelData);
        void LevelUpDash();
        void OpenDash(int level);
    }
}