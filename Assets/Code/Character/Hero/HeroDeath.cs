using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Services;
using UnityEngine;
using Zenject;


namespace Code.Character.Hero
{
    public class HeroDeath : MonoBehaviour, IHeroDeath
    {
        public event Action OnDeath;
        
        private IHero _hero;
        private MovementLimiter _limiter;
        private bool _isDeath;
        
        [Inject]
        private void Construct(AssetsConfig assetsConfig, MovementLimiter limiter)
        {
            _hero = GetComponent<IHero>();
            _hero.Health.OnHealthChanged += HealthChanged;
            _hero.Collision.OnWater += DeathOnWater;
            _limiter = limiter;
        }

        private void OnDestroy()
        {
            _hero.Health.OnHealthChanged -= HealthChanged;
            _hero.Collision.OnWater -= DeathOnWater;
        }

        private void HealthChanged()
        {
            if (_hero.Health.Current <= 0 && !_isDeath)
            {
                Die();
            }
        }

        private void Die()
        {
            _hero.Animator.PlayDeath();
            _hero.VFX.PlayDeathVFX(vfxPosition: transform.position - Vector3.right * transform.localScale.x * 0.5f);
            
            OnDeath?.Invoke();
            DisableHero();
        }
        
        private void DeathOnWater()
        {
            _hero.Animator.PlayDeathOnWater();
            _hero.VFX.PlayDeathVFX();
    
            OnDeath?.Invoke();
            DisableHero();
        }

        private void DisableHero()
        {
            _isDeath = true;

            _hero.Collision.DisableComponent();
            _hero.Movement.DisableComponent();
            _hero.Jump.DisableComponent();
            _hero.GunAttack.DisableComponent();
            _hero.HandAttack.DisableComponent();
            _limiter.DisableMovement();

            transform.position = new Vector3(transform.position.x, transform.position.y, Constants.SecondLayerPosition);
        }
    }
}