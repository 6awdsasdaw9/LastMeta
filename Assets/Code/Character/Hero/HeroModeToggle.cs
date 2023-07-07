using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Debugers;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroModeToggle: MonoBehaviour, IHeroModeToggle
    {
        public Constants.HeroMode Mode { get; private set; }
        private IHero _hero;
        private MovementLimiter _limiter;
        
        [Inject]
        private void Construct(MovementLimiter limiter)
        {
            _hero = GetComponent<IHero>();
            _limiter = limiter;
            SubscribeToEvents(true);
            SetDefaultMode();
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                
            _limiter.OnDisableMovementMode += OnDisableMovementMode;
            _limiter.OnEnableMovementMode += OnEnableMovementMode;
            }
            else
            {  _limiter.OnDisableMovementMode -= OnDisableMovementMode;
                _limiter.OnEnableMovementMode -= OnEnableMovementMode;
                
            }
        }

        public void SetDefaultMode()
        {
            Mode = Constants.HeroMode.Default;
            _hero.GunAttack?.Disable();
            _hero.HandAttack?.Enable();
            _hero.Animator.PlayEnterHandMode();
        }
        public void SetGunMode()
        {
            Mode = Constants.HeroMode.Gun;
            _hero.GunAttack?.Enable();
            _hero.HandAttack?.Disable();
            _hero.Animator.PlayEnterGunMode();
        }

        private void OnEnableMovementMode()
        {
            _hero.Movement.Enable();
            _hero.Jump.Enable();

            if (_hero.GameMode == Constants.GameMode.Game)
            {
                _hero.GunAttack?.Enable();
                _hero.HandAttack?.Enable();
            }
        }

        private void OnDisableMovementMode()
        {
            _hero.Movement.Disable();
            _hero.Jump.Disable();

            if (_hero.GameMode == Constants.GameMode.Game)
            {
                _hero.GunAttack?.Disable();
                _hero.HandAttack?.Disable();
            }
        }
    }
}