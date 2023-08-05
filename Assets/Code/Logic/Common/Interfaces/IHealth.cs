using System;
using Code.Data.GameData;

namespace Code.Logic.Common.Interfaces
{
    public interface IHealth
    {
        float Current { get; }
        float Max { get; }
        event Action OnHealthChanged;
        void TakeDamage(float damage);
    }

    public interface ICharacterHealth : IHealth
    {
        void Set(HealthData healthData);
        void Reset();
        void RestoreHealth(float health);
    }
}