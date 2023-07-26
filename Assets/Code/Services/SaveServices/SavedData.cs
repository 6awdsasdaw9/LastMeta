using System;
using System.Collections.Generic;
using Code.Character.Hero;
using Code.Data.Configs;
using Code.Data.GameData;

namespace Code.Services.SaveServices
{
    [Serializable]
    public class SavedData
    {
        public string CurrentScene;
        public TimeData TimeData;
        public HealthData HeroHealth;
        public HeroUpgradesLevelData HeroUpgradesLevel;
        public HeroAbilityLevelData HeroAbilityLevel;
        public PositionData HeroPosition;
        public PositionData CameraPosition;
        public Dictionary<string, Vector3Data> ObjectsPosition;
        public Dictionary<string, bool> DestroyedObjects;
        public Dictionary<string, bool> Items;
        public Dictionary<string, PointData> SceneSpawnPoints;
        public int Language;
        public int Money;


        public SavedData()
        {
            TimeData = new TimeData
            {
                Seconds =  0,
                Day = 1,
                TimeOfDay = TimeOfDay.Morning.ToString(),
            };
            HeroPosition = new PositionData();
            CameraPosition = new PositionData();
            HeroHealth = new HealthData();
            HeroUpgradesLevel = new HeroUpgradesLevelData();
            HeroAbilityLevel = new HeroAbilityLevelData()
            {
                HandLevel = 0,
                GunLevel =  -1,
                DashLevel = -1,
                SuperJumpLevel = -1
            };
            ObjectsPosition = new Dictionary<string, Vector3Data>();
            DestroyedObjects = new Dictionary<string, bool>();
            Items = new Dictionary<string, bool>();
            SceneSpawnPoints = new Dictionary<string, PointData>();
        }

    }

}