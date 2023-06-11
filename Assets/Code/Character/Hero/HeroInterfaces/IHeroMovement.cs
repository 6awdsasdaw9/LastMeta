using Code.Character.Common.CommonCharacterInterfaces;
using UnityEngine;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroMovement : IDisabledComponent
    {
        bool IsCrouch { get; }
        float DirectionX { get; }
        public void  BlockMovement();
        public void  UnBlockMovement();
        public void SetSupportVelocity(Vector2 otherObjectVelocity);
    }
}