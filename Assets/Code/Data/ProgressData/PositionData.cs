using System;
using Code.Data.GameData;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class PositionData
    {
        public string scene;
        public Vector3Data position;

        public PositionData()
        {
        }

        public PositionData(string scene, Vector3Data position)
        {
            this.scene = scene;
            this.position = position;
        }
    }
}