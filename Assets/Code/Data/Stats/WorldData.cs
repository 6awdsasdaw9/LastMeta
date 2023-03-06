using System;

namespace Code.Data.Stats
{
    [Serializable]
    public class WorldData
    {
        public HeroPositionData heroPositionData;

        public WorldData(string initialLevel)
        {
            heroPositionData = new HeroPositionData(initialLevel);
        }
    }
}