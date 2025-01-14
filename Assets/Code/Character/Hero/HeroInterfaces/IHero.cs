using Code.Logic.Common.Interfaces;
using UnityEngine;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHero
    {
        
        Constants.GameMode GameMode { get; }
        Transform Transform { get; }
        Rigidbody Rigidbody { get; } 
        IHeroStats Stats { get; }
        IHeroAnimator Animator { get; }
        IHeroAudio Audio { get; }
        IHeroCollision Collision { get;  }
        IHeroMovement Movement { get; }
        IHeroJump Jump { get; }


        IHeroModeToggle ModeToggle { get; }
        IHeroAttack HandAttack { get; }
        IHeroRangeAttack GunAttack { get; }
        IHeroBuff Buff { get; }
        ICharacterHealth Health { get; }
        IHeroDeath Death { get; }
        IHeroUpgrade Upgrade { get; }
        IHeroAbility Ability { get; }
        IHeroVFX VFX { get; }
        IHeroEffectsController EffectsController {get;}
    
    }
    
}