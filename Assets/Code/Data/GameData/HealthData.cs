using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.GameData
{
    [Serializable]
    public class HealthData
    {
        [ReadOnly] public float CurrentHP;
        public float MaxHP;

        public HealthData()
        {
            Reset();
        }

        public void Reset() => CurrentHP = MaxHP;
    }
}