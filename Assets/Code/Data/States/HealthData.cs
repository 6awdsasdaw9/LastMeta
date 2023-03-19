using System;

namespace Code.Data.States
{
    [Serializable]
    public class HealthData
    {
        public float currentHP;
        public float maxHP;

        public HealthData()
        {
 
            Reset();
        }
        
        public void Reset() => currentHP = maxHP;
    }
}