namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroAudio
    {
        void PlayStepSound();
        void PlaySoftStepSound();
        void PlayOnLandSound();
        void PlayPunchSound();
        void PlayDamageSound();
        void PlayJumpSound();
    }
}