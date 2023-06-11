using Code.Character.Common.CommonCharacterInterfaces;
using UnityEngine;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHero
    {
        Constants.GameMode Mode { get; }
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
        IHeroVFX VFX { get; }

    
    }
    
}