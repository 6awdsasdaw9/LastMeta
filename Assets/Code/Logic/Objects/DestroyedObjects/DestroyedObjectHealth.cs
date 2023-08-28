using System;
using Code.Data.GameData;
using Code.Logic.Common.Interfaces;
using UnityEngine;

namespace Code.Logic.Objects.DestroyedObjects
{
    public class DestroyedObjectHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthData _healthData;
        public float Current => _healthData.CurrentHP;
        public float Max => _healthData.MaxHP;
        public  Action OnHealthChanged { get; set; }

        private void OnEnable()
        {
            Reset();
        }

        public void Reset()
        { 
            _healthData.Reset();
        }

        public void TakeDamage(float damage)
        {
            _healthData.CurrentHP -= damage;
            OnHealthChanged?.Invoke();
        }
    }
}