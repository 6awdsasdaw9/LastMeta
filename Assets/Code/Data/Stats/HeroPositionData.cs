using System;
using UnityEngine;

namespace Code.Data.Stats
{
    [Serializable]
    public class HeroPositionData
    {
        public string level;
        public Vector3Data position;

        public HeroPositionData(string level, Vector3Data position)
        {
            this.level = level;
            this.position = position;
        }

        public HeroPositionData(string initialLevel)
        {
            level = initialLevel;
        }
    }
}