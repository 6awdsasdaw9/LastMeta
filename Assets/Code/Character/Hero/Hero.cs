using System;
using Code.Character.Common.CommonCharacterInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class Hero : MonoBehaviour, IHero
    {
        private bool _isNotRealHero => _gameMode != Constants.GameMode.Real;
        public Constants.GameMode GameMode => _gameMode;
        [SerializeField] private Constants.GameMode _gameMode;

        #region Common Value
        public IHeroAnimator Animator => _animator;
        [SerializeField] private HeroAnimator _animator;
        public IHeroAudio Audio => _audio;
        [SerializeField] private HeroAudio _audio;
        public IHeroMovement Movement => _movement;
        [SerializeField] private HeroMovement _movement;
        public IHeroJump Jump => _jump;
        [SerializeField] private HeroJump _jump;
        public IHeroCollision Collision => _collision;
        [SerializeField] private HeroCollision _collision;
        public Transform Transform => transform;
        private MovementLimiter _movementLimiter;
        
        #endregion

        #region Game Value

        public IHeroMode HeroMode => _heroMode;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroMode _heroMode;
        public IHeroAttack HandAttack => _attack;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroAttack _attack;

        public IHeroAttack GunAttack => _heroShooting;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroShooting _heroShooting;
        
        public IHeroBuff Buff => _buff;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroBuff _buff;
        public IHeroDeath Death => _death;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroDeath _death;
        public IHealth Health => _health;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroHealth _health;
        public IHeroUpgrade Upgrade => _upgrade;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroUpgrade _upgrade;


        public IHeroAbility Ability => _ability;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroAbility _ability;
        public IHeroVFX VFX => _vfx;
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroVFX _vfx;
        
        #endregion


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

            if (_isNotRealHero)
            {
                _attack.Enable();
            }
        }

        private void OnDisableMovementMode()
        {
            _movement.Disable();
            _jump.Disable();
            if (_isNotRealHero)
            {
                _attack.Disable();
            }
        }
    }
}