using System;
using Code.Data;
using Code.Data.SavedDataPersistence;
using Code.Data.Stats;
using UnityEngine;

namespace Code.Character.Hero
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; set; }
        float Max { get; set; }
        void TakeDamage(float damage);
    }

    public class HeroHealth : MonoBehaviour, IHealth, ISavedData
    {
        private HealthData _healthData;
        public event Action HealthChanged;

        public float Current
        {
            get => _healthData.currentHP;
            set
            {
                if (_healthData.currentHP != value)
                {
                    _healthData.currentHP = value;
                }
            }
        }

        public float Max
        {
            get => _healthData.maxHP;
            set => _healthData.maxHP = value;
        }


        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;

            HealthChanged?.Invoke();
        }

        public void LoadData(SavedData savedData)
        {
            _healthData = savedData.heroHealth;
            HealthChanged?.Invoke();
        }

        public void SaveData(SavedData savedData)
        {
            savedData.heroHealth.currentHP = Current;
            savedData.heroHealth.maxHP = Max;
        }
    }
}