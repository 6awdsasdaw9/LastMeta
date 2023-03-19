using System;
using Code.Data.States;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class SavedData
    {
        public HealthData heroHealth;
        public HeroPositionData heroPositionData;
        public PowerData heroPowerData;

        public float currentTime;
        public SavedData()
        {
            heroPositionData = new HeroPositionData();
            heroHealth = new HealthData();
        }
    }
}