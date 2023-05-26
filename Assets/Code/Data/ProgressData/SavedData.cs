using System;
using System.Collections.Generic;
using Code.Character.Hero;
using Code.Data.GameData;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class SavedData
    {
        public string CurrentScene;
        public HealthData HeroHealth;
        public HeroUpgradesData HeroUpgradesLevel;
        public PositionData HeroPosition;
        public PositionData CameraPosition;
        public Dictionary<string, Vector3Data> ObjectsPosition;

        public float currentTime;
        public SavedData()
        {
            HeroPosition = new PositionData();
            CameraPosition = new PositionData();
            HeroHealth = new HealthData();
            HeroUpgradesLevel = new HeroUpgradesData();
            ObjectsPosition = new Dictionary<string, Vector3Data>();
        }
    }

}