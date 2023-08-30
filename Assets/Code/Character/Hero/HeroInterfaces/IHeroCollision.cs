using System;
using Code.Logic.Common.Interfaces;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroCollision :IDisabledComponent
    {
        bool OnGround { get; }
        bool UnderCeiling { get; }
         bool OnMovementPlatform { get; }
        event Action OnWater;

        void SetFrictionPhysicsMaterial();
        void SetNoFrictionPhysicsMaterial();
    }
}