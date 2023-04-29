using Code.Character.Hero.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Character.Hero
{
    public interface IHeroMovement : IDisabled
    {
        bool IsCrouch { get; }
        float DirectionX { get; }
        public void  BlockMovement();
        public void  UnBlockMovement();
        public void SetSupportVelocity(Vector2 otherObjectVelocity);
    }
}