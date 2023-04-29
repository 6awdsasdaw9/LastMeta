namespace Code.Character.Hero
{
    public interface IHeroAnimator 
    {
        void PlayJump();
        void PlayMove();
        void PlayCrouch();
        void PlayAttack();
        void PlayDeath();
        void PlayDeathOnWater();
        void PlayDamageFromAbove();
        void PlayStunned(bool isStunned);
    }
}