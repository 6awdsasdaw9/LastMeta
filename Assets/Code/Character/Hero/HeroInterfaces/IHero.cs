using Code.Character.Interfaces;
using UnityEngine;

namespace Code.Character.Hero
{
    public interface IHero
    {
        Transform Transform { get; }
        IHeroAnimator Animator { get; }
        IHeroAttack Attack { get; }
        IHeroAudio Audio { get; }
        IHeroBuff Buff { get; }
        IHeroCollision Collision { get; }
        IHeroDeath Death { get; }
        IHealth Health { get; }
        IHeroMovement Movement { get; }
        IHeroJump Jump { get; }
        IHeroUpgrade Upgrade { get; }
    }
}