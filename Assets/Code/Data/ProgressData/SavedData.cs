using System;
using Code.Data.GameData;
using Code.Data.States;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class SavedData
    {
        public HealthData heroHealth;
        public PositionData heroPositionData;
        public PositionData cameraPositionData;
        public PowerData heroPowerData;

        public HeroParamData heroParamData;

        public float currentTime;
        public SavedData()
        {
            heroPositionData = new PositionData();
            cameraPositionData = new PositionData();
            heroHealth = new HealthData();
            heroParamData = new HeroParamData();
        }
    }


}