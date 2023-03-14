using System;
using Code.Data.Stats;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class SavedData
    {
        public HealthData heroHealth;
        public HeroScenePositionData heroScenePositionData;
        public PowerData heroPowerData;

        public float currentTime;

        public SavedData(string initialScene)
        {
            heroScenePositionData = new HeroScenePositionData(initialScene);
        }
    }
}