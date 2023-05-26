using System;
using Code.Character.Interfaces;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent((typeof(EnemyAnimator)))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _current;
        [SerializeField] private float _max;

        public event Action HealthChanged;

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
        public void Set(float currentHealth, float maxHealth)
        {
            
        }

        public void Reset()
        {
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }
    }
}