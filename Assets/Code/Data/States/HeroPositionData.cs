using System;

namespace Code.Data.States
{
    [Serializable]
    public class HeroPositionData
    {
        public string level;
        public Vector3Data position;

        public HeroPositionData()
        {
        }

        public HeroPositionData(string level, Vector3Data position)
        {
            this.level = level;
            this.position = position;
        }
    }
}