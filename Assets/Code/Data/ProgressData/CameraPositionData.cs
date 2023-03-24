using System;
using Code.Data.States;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class CameraPositionData : PositionData
    {
        public bool isCanMove;

        public CameraPositionData()
        {
            isCanMove = true;
        }

        public CameraPositionData(string level, Vector3Data position, bool canMove)
        {
            this.level = level;
            this.position = position;
            isCanMove = canMove;
        }
    }
}