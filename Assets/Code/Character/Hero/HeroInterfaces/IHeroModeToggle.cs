namespace Code.Character.Hero
{
    public interface IHeroModeToggle
    {
        public Constants.HeroMode Mode { get; }
        void SetDefaultMode();
        void SetGunMode();
    }
}