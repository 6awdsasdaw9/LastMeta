namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroAudio
    {
        void PlayStepSound();
        void PlaySoftStepSound();
        void PlayOnLandAudio();
        void PlayPunchAudio();
        void PlayDamageAudio();
        void PlayJump();
    }
}