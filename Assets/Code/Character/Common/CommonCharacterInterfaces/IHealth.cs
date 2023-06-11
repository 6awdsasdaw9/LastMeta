using System;
using Code.Data.GameData;

namespace Code.Character.Common.CommonCharacterInterfaces
{
    public interface IHealth
    {
        
        event Action HealthChanged;
        float Current { get; }
        float Max { get;  }

        void Set(HealthData healthData);
        void Reset();
        void TakeDamage(float damage);
    }
}