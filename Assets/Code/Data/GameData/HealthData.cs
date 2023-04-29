using System;

namespace Code.Data.GameData
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