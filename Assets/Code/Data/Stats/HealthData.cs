
using System;

namespace Code.Data.Stats
{
    [Serializable]
    public class HealthData
    {
        public float currentHP;
        public float maxHP;

        public void Reset() => currentHP = maxHP;
    }
}