namespace Code.Character.Hero
{
    public interface IHeroStats
    {
        Constants.HeroMode Mode { get; }
        bool IsBlockMove { get; }
        bool IsDash { get; }
        bool IsMove { get; }
        bool IsCrouch { get; }
        bool IsAttack { get; }
        bool IsDeath { get; }
        bool IsJump { get; }
        float CurrentHeath { get; }
        float MaxHeath { get; }
        float BonusHealth { get; }
        float Damage { get; }
        float ModeSpeedMultiplayer { get; }
        float Speed { get; }
        float JumpHeight { get; }
        int AirJump { get; }
    }
}