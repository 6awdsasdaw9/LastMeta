namespace Code.Character.Hero
{
    public interface IHeroUpgrade
    {
        public HeroUpgradesData UpgradesLevel { get; }
        public void Init(HeroUpgradesData heroUpgradesData);
        float BonusSpeed { get; }
        float BonusHeightJump { get; }
        int BonusAirJump { get; }
    }
}