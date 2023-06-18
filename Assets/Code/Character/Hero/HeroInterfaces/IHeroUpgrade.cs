namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroUpgrade
    {
        public HeroUpgradesLevelData UpgradesLevelUpgradesLevel { get; }
        public void Init(HeroUpgradesLevelData heroUpgradesLevelData);
        float BonusSpeed { get; }
        float BonusHeightJump { get; }
        int BonusAirJump { get; }
    }
}