using System;
using Code.Data.GameData;
using Code.Data.Stats;
using Code.Debugers;
using Zenject;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class SavedData
    {
        public HealthData heroHealth;
        public HeroScenePositionData heroScenePositionData;
        public PowerData heroPowerData;

        public float currentTime;

    
        
        public SavedData( )
        {
            heroScenePositionData = new HeroScenePositionData(Constants.initialScene);
            heroHealth = new HealthData(20);
        }
    }
}