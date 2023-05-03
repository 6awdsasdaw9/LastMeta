using Code.Character.Interfaces;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class Hero : MonoBehaviour, IHero
    {
        //Common
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroAudio _audio;
        [SerializeField] private HeroMovement _movement;
        [SerializeField] private HeroJump _jump;
        [SerializeField] private HeroCollision _collision;
       
        [Space] //Game Hero
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroBuff _buff;
        [SerializeField] private HeroDeath _death;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroUpgrade _upgrade;
        [SerializeField] private HeroVFX _vfx;

        public Transform Transform => transform;
        public IHeroAnimator Animator => _animator;
        public IHeroAudio Audio => _audio;
        public IHeroMovement Movement => _movement;
        public IHeroJump Jump => _jump;
        public IHeroCollision Collision => _collision;
        
        public IHeroAttack Attack => _attack;
        
        public IHeroBuff Buff => _buff;
        public IHeroDeath Death => _death;
        public IHealth Health => _health;
        public IHeroUpgrade Upgrade => _upgrade;
        public IHeroVFX VFX => _vfx;
        
        private MovementLimiter _movementLimiter;

        [Inject]
        private void Construct(MovementLimiter movementLimiter)
        {
            _movementLimiter = movementLimiter;
            _movementLimiter.OnDisableMovementMode += OnDisableMovementMode;
            _movementLimiter.OnEnableMovementMode += OnEnableMovementMode;
        }

        private void OnEnableMovementMode()
        {
            _movement.Enable();
            _jump.Enable();
            _attack?.Enable();
        }

        private void OnDisableMovementMode()
        {
            _movement.Disable();
            _jump.Disable();
            _attack?.Disable(); //TODO непотребство
        }
    }
}