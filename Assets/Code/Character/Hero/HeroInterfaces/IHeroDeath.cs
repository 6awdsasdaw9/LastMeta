using System;

namespace Code.Character.Hero
{
    public interface IHeroDeath
    {
        event Action OnDeath;
    }
}