using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.GameData;
using Code.Logic.DayOfTime;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "ScriptableObjects/GameData/ScenesConfig")]
    public class ScenesConfig : ScriptableObject
    {
        public Constants.Scenes InitialScene;
        public float DayTimeInSeconds = 50;

        [ListDrawerSettings(Expanded = false, ShowIndexLabels = true, ShowPaging = false, ShowItemCount = true)]
        public List<SceneParams> ScenesParams;

        public SceneParams GetSceneParam(string sceneName)
        {
            return ScenesParams.FirstOrDefault(p => p.Scene.ToString() == sceneName);
        }
    }

    [Serializable]
    public class SceneParams
    {
        [GUIColor(0, 0.5f, 0.5f, 0.8f)] public Constants.Scenes Scene;
        [EnumToggleButtons] public Constants.GameMode GameMode;

        [Title("Music")] 
        public EventReference Music;
        public EventReference Ambience;

        [Title("Lighting")] 
        public TimeOfDaySettings TimeOfDaySettings;
        
        public List<PointData> Points;
    }

    [Serializable]
    public class PointData
    {
        public int ID;
        public Vector3 Position;
    }

    [Serializable]
    public class TimeOfDaySettings
    {
        public bool IsEmpty => LightParams.Length == 0;

        public LightParams[] LightParams =
        {
            new LightParams()
            {
                TimeOfDay = TimeOfDay.Morning,
                DirectionLightIntensity = 0.9f,
                Duration = 0.3f,
                DirectionLightAngle = new Vector3(5, -2, 0),
            },
            new LightParams()
            {
                TimeOfDay = TimeOfDay.Evening,
                DirectionLightIntensity = 0.45f,
                Duration = 0.5f,
                DirectionLightAngle = new Vector3(-35, 7, 0),
            },
            new LightParams()
            {
                TimeOfDay = TimeOfDay.Night,
                Duration = 1f,
                DirectionLightIntensity = 0.15f,
                DirectionLightAngle = new Vector3(-55, 13, 0),
            },
        };

        public LightParams GetLightParams(TimeOfDay timeOfDay)
        {
            return LightParams.FirstOrDefault(p => p.TimeOfDay == timeOfDay);
        }
    }

    [Serializable]
    public class LightParams
    {
        public TimeOfDay TimeOfDay;
        [Range(0,1)]public float Duration;
        public float DirectionLightIntensity;
        public Vector3 DirectionLightAngle;
    }
}