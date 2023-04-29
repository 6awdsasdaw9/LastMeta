namespace Code.Character.Hero
{
    public interface IHeroUpgrade
    {
        float BonusSpeed { get; }
        float BonusHeightJump { get; }
        int BonusAirJump { get; }
    }
}