using System;
using Code.Character.Common;
using Code.Character.Interfaces;
using Code.Data.GameData;
using UnityEngine;

namespace Code.Character.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HeroAudio _heroAudio;
        [SerializeField] private SpriteVFX spriteVFX;
        private HealthData _healthData;
        public event Action HealthChanged;
        
        public float Current => _healthData.CurrentHP;
        public float Max => _healthData.MaxHP;
        
        public void Set(float currentHealth, float maxHealth)
        {
            _healthData = new HealthData()
            {
                CurrentHP = currentHealth,
                MaxHP = maxHealth
            };
            HealthChanged?.Invoke();
        }

        public void Reset()
        {
            _healthData.Reset();
            HealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            _heroAudio.PlayDamageAudio();
            spriteVFX.RedColorize();
            
            //TODO можно уйти в минус.
            _healthData.CurrentHP -= damage;
            
            HealthChanged?.Invoke();
        }
        
    }
}