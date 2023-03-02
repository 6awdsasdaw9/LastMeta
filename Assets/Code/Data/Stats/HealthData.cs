
using System;

namespace Code.Data
{
    [Serializable]
    public class HealthData
    {
        public float currentHP;
        public float maxHP;

        public void Reset() => currentHP = maxHP;
    }
}