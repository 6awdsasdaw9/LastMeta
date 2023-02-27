using System;

namespace Code.Data.DataPersistence
{
    [Serializable]
    public class ProgressData
    {
        public HealthData heroHealth;
        public WorldData worldData;
        public PowerData heroPowerData;
    }
}