using System;

namespace Code.Character.Interfaces
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; }
        float Max { get;  }

        void Set(float currentHealth, float maxHealth);
        void Reset();
        void TakeDamage(float damage);
    }
}