using Code.Character.Hero.HeroInterfaces;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroModeToggle : MonoBehaviour, IHeroModeToggle
    {
        public Constants.HeroMode Mode { get; private set; }
        private MovementLimiter _limiter;
        
        private IHero _hero;
        private Rigidbody _heroRigidbody;

        [Inject]
        private void Construct(MovementLimiter limiter)
        {
            _hero = GetComponent<IHero>();
            _hero.Transform.gameObject.TryGetComponent(out _heroRigidbody);
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
            {
                _limiter.OnDisableMovementMode -= OnDisableMovementMode;
                _limiter.OnEnableMovementMode -= OnEnableMovementMode;
            }
        }

        public void SetDefaultMode()
        {
            Mode = Constants.HeroMode.Default;
            
            _hero.GunAttack.DisableComponent();
            _hero.HandAttack.EnableComponent();

            if (_hero.Animator.IsCalPlayAnimation) _hero.Animator.PlayEnterHandMode();
        }

        public void SetGunMode()
        {
            Mode = Constants.HeroMode.Gun;
            
            _hero.GunAttack.EnableComponent();
            _hero.HandAttack.DisableComponent();
            
            if (_hero.Animator.IsCalPlayAnimation) _hero.Animator.PlayEnterGunMode();
        }
        

        private void OnEnableMovementMode()
        {
            _hero.Movement.EnableComponent();
            _hero.Jump.EnableComponent();
            _heroRigidbody.isKinematic = false;

            if (_hero.GameMode != Constants.GameMode.Game) return;

            _hero.VFX.SpriteVFX.SetDefaultMaterial();
            _hero.GunAttack?.EnableComponent();
            _hero.HandAttack?.EnableComponent();
        }

        private void OnDisableMovementMode()
        {
            _hero.Movement.DisableComponent();
            _hero.Jump.DisableComponent();
            _heroRigidbody.isKinematic = true;

            if (_hero.GameMode != Constants.GameMode.Game) return;

            _hero.VFX.SpriteVFX.SetGlitchMaterial();
            _hero.GunAttack?.DisableComponent();
            _hero.HandAttack?.DisableComponent();
        }
    }
}