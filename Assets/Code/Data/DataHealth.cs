
using System;

namespace Code.Data
{
    [Serializable]
    public class DataHealth
    {
        public float currentHP;
        public float maxHP;

        public void Reset() => currentHP = maxHP;
    }
}