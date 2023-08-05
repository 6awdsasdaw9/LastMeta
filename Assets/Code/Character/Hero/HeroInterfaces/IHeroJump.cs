using Code.Logic.Common.Interfaces;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroJump: IDisabledComponent
    {
        public bool IsCurrentlyJumping { get; }
         float Height { get; }
    }
}