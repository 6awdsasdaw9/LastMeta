using System;

namespace Code.Data.GameData
{
    [Serializable]
    public class HealthData
    {
        public float CurrentHP;
        public float MaxHP;

        public HealthData()
        {
            Reset();
        }

        public void Reset() => CurrentHP = MaxHP;
    }
}