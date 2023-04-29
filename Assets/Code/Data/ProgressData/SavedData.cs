using System;
using System.Collections.Generic;
using Code.Character.Hero;
using Code.Data.GameData;
using HeroParamData = Code.Data.GameData.HeroParamData;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class SavedData
    {
        public HealthData heroHealth;
        public HeroUpgradesData HeroUpgradesData;
        public PositionData heroPositionData;
        public PositionData cameraPositionData;
        public PowerData heroPowerData;
        public Dictionary<UniqueId, Vector3Data> ObjectsPosition;

        public float currentTime;
        public SavedData()
        {
            heroPositionData = new PositionData();
            cameraPositionData = new PositionData();
            heroHealth = new HealthData();
            HeroUpgradesData = new HeroUpgradesData();
        }
    }

}