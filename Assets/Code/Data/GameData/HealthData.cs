using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.GameData
{
    [Serializable]
    public class HealthData
    {
        public float MaxHP;
        [HideInInspector] public float CurrentHP;

  

        public void Reset() => CurrentHP = MaxHP;
    }
}