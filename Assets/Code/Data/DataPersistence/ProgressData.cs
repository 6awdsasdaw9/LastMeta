using System;
using Code.Data.Stats;

namespace Code.Data.DataPersistence
{
    [Serializable]
    public class ProgressData
    {
        public HealthData heroHealth;
        public WorldData worldData;
        public PowerData heroPowerData;

        public ProgressData(string initialScene)
        {
            worldData = new WorldData(initialScene);
        }
    }
}