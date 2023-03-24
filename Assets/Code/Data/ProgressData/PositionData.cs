using System;
using Code.Data.States;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class PositionData
    {
        public string level;
        public Vector3Data position;

        public PositionData()
        {
        }

        public PositionData(string level, Vector3Data position)
        {
            this.level = level;
            this.position = position;
        }
    }
}