using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Common.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroHealth : MonoBehaviour, ICharacterHealth
    {
        private IHero _hero;
        private HealthData _healthData = new();
        public event Action OnHealthChanged;

        [Inject]
        private void Construct()
        {
            _hero = GetComponent<IHero>();
        }
        
        public float Max => _healthData.MaxHP + _hero.Stats.BonusHealth;
        public float Current
        {
            get => _healthData.CurrentHP;
            private set
            {
                if (_healthData.CurrentHP != value)
                {
                    _healthData.CurrentHP = value;
                }
            }
        }


        public void Set(HealthData healthData)
        {
            _healthData = healthData;
            OnHealthChanged?.Invoke();
        }

        public void Reset()
        {
            _healthData.Reset();
            OnHealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0 || damage <= 0)
                return;

            _hero.Audio.PlayDamageSound();
            _hero.VFX.SpriteVFX.RedColorize();
            
            Current -= damage;
            
            if (Current < 0)
            {
                Current = 0;
            }
            
            OnHealthChanged?.Invoke();
        }

        public void RestoreHealth(float health)
        {
            Current += health;
            if (_healthData.CurrentHP > _healthData.MaxHP)
            {
                Current = Max;
            }
        }
    }
}