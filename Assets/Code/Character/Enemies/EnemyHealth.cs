using System;
using Code.Character.Common.CommonCharacterInterfaces;
using Code.Data.GameData;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent((typeof(EnemyAnimator)))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _current;
        [SerializeField] private float _max;

        public event Action OnHealthChanged;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        //TODO допилить
        public void Set(HealthData healthData)
        {
            
        }

        public void Reset()
        {
            _current = _max;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            OnHealthChanged?.Invoke();
        }

        public void RestoreHealth(float health)
        {
            _current += health;
            if (_current > _max)
            {
                _current = _max;
            }
        }
    }
}