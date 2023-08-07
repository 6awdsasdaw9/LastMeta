using Code.Logic.Common.Interfaces;
using UnityEngine;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroMovement : IDisabledComponent
    {
        bool IsCrouch { get; }
        float DirectionX { get; }
        float Speed { get; }

        public void  BlockMovement();
        public void  UnBlockMovement();
        public void SetSupportVelocity(Vector2 otherObjectVelocity);
        public void AddBonusSpeed(float bonusSpeed);
    }
}