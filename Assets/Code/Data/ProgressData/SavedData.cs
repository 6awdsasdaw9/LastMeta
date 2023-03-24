using System;
using Code.Data.States;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class SavedData
    {
        public HealthData heroHealth;
        public PositionData heroPositionData;
        public CameraPositionData cameraPositionData;
        
        public PowerData heroPowerData;

        public float currentTime;
        public SavedData()
        {
            heroPositionData = new PositionData();
            cameraPositionData = new CameraPositionData();
            heroHealth = new HealthData();
        }
    }


}