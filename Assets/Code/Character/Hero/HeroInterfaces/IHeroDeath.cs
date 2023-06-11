using System;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroDeath
    {
        event Action OnDeath;
    }
}