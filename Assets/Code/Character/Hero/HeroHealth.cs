using System;
using Code.Character.Common;
using Code.Character.Common.CommonCharacterInterfaces;
using Code.Data.GameData;
using UnityEngine;

namespace Code.Character.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HeroAudio _heroAudio;
        [SerializeField] private SpriteVFX spriteVFX;
        private HealthData _healthData = new();
        public event Action HealthChanged;
        
        public float Current
        {
            get => _healthData.CurrentHP;
            set
            {
                if (_healthData.CurrentHP != value)
                {
                    _healthData.CurrentHP = value;
                }
            }
        }

        public float Max
        {
            get => _healthData.MaxHP;
            set => _healthData.MaxHP = value;
        }
        public void Set(HealthData healthData)
        {
            _healthData = healthData;
            HealthChanged?.Invoke();
        }

        public void Reset()
        {
            _healthData.Reset();
            HealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0 || damage <= 0)
                return;

            _heroAudio.PlayDamageAudio();
            spriteVFX.RedColorize();
            
            _healthData.CurrentHP -= damage;
            
            if (_healthData.CurrentHP < 0)
            {
                _healthData.CurrentHP = 0;
            }
            
            HealthChanged?.Invoke();
        }

        public void RestoreHealth(float health)
        {
            _healthData.CurrentHP += health;
            if (_healthData.CurrentHP > _healthData.MaxHP)
            {
                _healthData.CurrentHP = _healthData.MaxHP;
            }
        }
    }
}