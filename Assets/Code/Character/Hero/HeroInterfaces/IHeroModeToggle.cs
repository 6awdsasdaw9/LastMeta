namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroModeToggle
    {
        public Constants.HeroMode Mode { get; }
        void SetDefaultMode();
        void SetGunMode();
    }
}