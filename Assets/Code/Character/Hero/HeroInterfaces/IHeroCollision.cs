using System;
using Code.Character.Hero.Interfaces;

namespace Code.Character.Hero
{
    public interface IHeroCollision :IDisabled
    {
        bool OnGround { get; }
        bool UnderCeiling { get; }
        event Action OnWater;

        void SetFrictionPhysicsMaterial();
        void SetNoFrictionPhysicsMaterial();
    }
}