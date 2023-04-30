using Code.Character.Interfaces;
using UnityEngine;

namespace Code.Character.Hero
{
    public interface IHero
    {
        Transform Transform { get; }
        IHeroAnimator Animator { get; }
        IHeroAudio Audio { get; }
        IHeroCollision Collision { get;  }
        IHeroMovement Movement { get; }
        IHeroJump Jump { get; }
        
        
        IHeroAttack Attack { get; }
        IHeroBuff Buff { get; }
        IHealth Health { get; }
        IHeroDeath Death { get; }
        IHeroUpgrade Upgrade { get; }
    }
    
}