namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroStats
    {
        Constants.HeroMode Mode { get; }
        int Direction { get; }
        bool IsBlockMove { get; }
        bool IsDash { get; }
        bool IsMove { get; }
        bool IsCrouch { get; }
        bool IsAttack { get; }
        bool IsDeath { get; }
        bool IsJump { get; }

        bool IsWounded { get; }
        bool OnGround { get; }


        float CurrentHeath { get; }
        float MaxHeath { get; }
        float BonusHealth { get; }
        float Damage { get; }
        float ModeSpeedMultiplayer { get; }
        float SerfaceSpeedMultiplayer{ get; }
        float Speed { get; }
        float JumpHeight { get; }
        int AirJump { get; }
        float MelleAttackSpeed { get; }
    }
}