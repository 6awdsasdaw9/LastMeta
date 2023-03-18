
using System;
using Code.Data.GameData;
using Sirenix.Serialization;
using Zenject;

namespace Code.Data.Stats
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