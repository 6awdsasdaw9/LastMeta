using System;

namespace Code.Data.Stats
{
    [Serializable]
    public class HeroScenePositionData
    {
        public HeroPositionData heroPositionData;

        public HeroScenePositionData(string initialLevel)
        {
            heroPositionData = new HeroPositionData(initialLevel);
        }
    }
}