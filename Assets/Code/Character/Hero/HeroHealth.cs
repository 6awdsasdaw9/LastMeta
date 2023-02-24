using System;
using Code.Data;
using UnityEngine;

namespace Code.Character
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; set; }
        float Max { get; set; }
        void TakeDamage(float damage);
    }

    public class HeroHealth : MonoBehaviour, IHealth
    {
        private DataHealth _dataHealth;
        public event Action HealthChanged;

        public float Current
        {
            get => _dataHealth.currentHP;
            set
            {
                if (_dataHealth.currentHP != value)
                {
                    _dataHealth.currentHP = value;
                }
            }
        }

        public float Max
        {
            get => _dataHealth.maxHP;
            set => _dataHealth.maxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _dataHealth = progress.heroDataHealth;

            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.heroDataHealth.currentHP = Current;
            progress.heroDataHealth.maxHP = Max;
        }


        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;

            HealthChanged?.Invoke();
        }
    }
}