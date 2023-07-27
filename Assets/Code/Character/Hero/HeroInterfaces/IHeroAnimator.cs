namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroAnimator 
    {
        bool IsCalPlayAnimation { get; }
        void PlayJump();
        void PlayMove();
        void PlayCrouch();
        void PlayAttack();
        void PlayStopAttack();
        void PlayDeath();
        void PlayDeathOnWater();
        void PlayDash(bool isDash);
        void PlayDamageFromAbove();
        void PlayStunned(bool isStunned);
        void PlayEnterGunMode();
        void PlayEnterHandMode();
    }
}