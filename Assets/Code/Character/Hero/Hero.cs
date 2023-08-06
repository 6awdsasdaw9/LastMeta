using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs.HeroConfigs;
using Code.Logic.Common.Interfaces;
using Code.Services;
using Code.Services.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class Hero : MonoBehaviour, IHero
    {
        private bool _isGameHero => _gameMode != Constants.GameMode.Real;
        public Constants.GameMode GameMode => _gameMode;
        [SerializeField] private Constants.GameMode _gameMode;

        #region Common Value
        public Transform Transform => transform;
        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] private Rigidbody _rigidbody;
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
        #endregion

        #region Game Value
        public IHeroModeToggle ModeToggle => _modeToggle;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroModeToggle _modeToggle;
        public IHeroAttack HandAttack => _attack;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroAttack _attack;
        public IHeroRangeAttack GunAttack => _shooting;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroShooting _shooting;
        
        public IHeroBuff Buff => _buff;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroBuff _buff;
        public IHeroDeath Death => _death;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroDeath _death;
        public ICharacterHealth Health => _health;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroHealth _health;
        public IHeroUpgrade Upgrade => _upgrade;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroUpgrade _upgrade;
        public IHeroAbility Ability => _ability;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroAbility _ability;
        public IHeroEffectsController EffectsController => _effectsController;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroEffectsController _effectsController; 

        public IHeroVFX VFX => _vfx;
        [ShowIf(nameof(_isGameHero)), SerializeField]
        private HeroVFX _vfx;
        public IHeroStats Stats { get; private set; } 
        #endregion

        [Inject]
        private void Construct(MovementLimiter movementLimiter, HeroConfig heroConfig)
        {
            Stats = _isGameHero
                ? new HeroGameStats(this, movementLimiter, heroConfig)
                : new HeroStats(this, movementLimiter, heroConfig);
        }
    }
}