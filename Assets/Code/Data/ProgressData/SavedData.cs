using System;
using Code.Data.Stats;

namespace Code.Data.SavedDataPersistence
{
    [Serializable]
    public class SavedData
    {
        public HealthData heroHealth;
        public WorldData worldData;
        public PowerData heroPowerData;

        public float currentTime;

        public SavedData(string initialScene)
        {
            worldData = new WorldData(initialScene);
        }
    }
}