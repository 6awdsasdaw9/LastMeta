using Code.Data.Configs;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroUpgrade
    {
        public HeroUpgradesLevelData UpgradesLevel { get; }
        public void Init(HeroUpgradesLevelData heroUpgradesLevelData);
        float BonusSpeed { get; }
        float BonusHeightJump { get; }
        int BonusAttack { get; }
        float BonusHealth { get; }
    }
}