using Code.Character.Common.CommonCharacterInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class Hero : MonoBehaviour, IHero
    {
        //Common
        [SerializeField] private Constants.GameMode _gameMode;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroAudio _audio;
        [SerializeField] private HeroMovement _movement;
        [SerializeField] private HeroJump _jump;
        [SerializeField] private HeroCollision _collision;

        [Space] //Game Hero
        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroAttack _attack;

        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroBuff _buff;

        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroDeath _death;

        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroHealth _health;

        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroUpgrade _upgrade;

        [ShowIf(nameof(_isNotRealHero)), SerializeField]
        private HeroVFX _vfx;

        private bool _isNotRealHero => _gameMode != Constants.GameMode.Real;
        public Constants.GameMode Mode => _gameMode;
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