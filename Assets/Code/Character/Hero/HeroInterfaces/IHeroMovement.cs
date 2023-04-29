using Code.Character.Hero.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Character.Hero
{
    public interface IHeroMovement : IDisabled
    {
        bool IsCrouch { get; }
        float DirectionX { get; }
        public UniTaskVoid BlockMovement(bool unblockCondition);
        public void SetSupportVelocity(Vector2 otherObjectVelocity);
    }
}