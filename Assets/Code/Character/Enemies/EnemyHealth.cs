using System;
using Code.Character.Common.CommonCharacterInterfaces;
using Code.Data.GameData;
using TMPro;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent((typeof(EnemyAnimator)))]
    public class EnemyHealth : MonoBehaviour, ICharacterHealth
    {
        private readonly HealthData _data = new();
        public event Action OnHealthChanged;

        public float Current
        {
            get => _data.CurrentHP;
            private set => _data.CurrentHP = value;
        }

        public float Max
        {
            get => _data.MaxHP;
            private set => _data.MaxHP = value;
        }
        
        public void Set(HealthData healthData)
        {
            Max = healthData.MaxHP;
            Reset();
        }

        public void Reset()
        {
            _data.CurrentHP = _data.MaxHP;
            OnHealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            OnHealthChanged?.Invoke();
        }

        public void RestoreHealth(float health)
        {
            _data.CurrentHP += health;
            if (_data.CurrentHP > _data.MaxHP)
            {
                _data.CurrentHP = _data.MaxHP;
            }
        }
    }
}