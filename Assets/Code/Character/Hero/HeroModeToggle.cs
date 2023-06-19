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

        private void SubscribeToEvents(bool flag)
        {
            _limiter.OnDisableMovementMode += OnDisableMovementMode;
            _limiter.OnEnableMovementMode += OnEnableMovementMode;
        }

        public void SetDefaultMode()
        {
            Logg.ColorLog("HeroMode: Set Hand");
            Mode = Constants.HeroMode.Default;
            _hero.GunAttack.Disable();
            _hero.HandAttack.Enable();
            _hero.Animator.PlayEnterHandMode();
        }
        public void SetGunMode()
        {
            Logg.ColorLog("HeroMode: Set Gun");
            Mode = Constants.HeroMode.Gun;
            _hero.GunAttack.Enable();
            _hero.HandAttack.Disable();
            _hero.Animator.PlayEnterGunMode();
        }

        private void OnEnableMovementMode()
        {
            _hero.Movement.Enable();
            _hero.Jump.Enable();

            if (_hero.GameMode == Constants.GameMode.Game)
            {
                _hero.GunAttack.Enable();
                _hero.HandAttack.Enable();
            }
            Logg.ColorLog("Hero EnableMovementMode");
        }

        private void OnDisableMovementMode()
        {
            _hero.Movement.Disable();
            _hero.Jump.Disable();

            if (_hero.GameMode == Constants.GameMode.Game)
            {
                _hero.GunAttack.Disable();
                _hero.HandAttack.Disable();
            }
            Logg.ColorLog("Hero DisableMovementMode");
        }
    }
}