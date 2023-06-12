using System;
using System.Linq;
using Code.Data.GameData;
using UnityEngine;

namespace Code.Logic.DayOfTime
{
    [Serializable]
    public class TimeOfDaySettings
    {
        public bool IsEmpty => LightParams.Length == 0;
        public LightParams[] LightParams =
        {
            new LightParams()
            {
                TimeOfDay =TimeOfDay.Morning,
                DirectionLightIntensity = 0.9f,
                DirectionLightAngle = new Vector3(5, -2, 0),
            },
            new LightParams()
            {
                TimeOfDay =TimeOfDay.Evening,
                DirectionLightIntensity = 0.45f,
                DirectionLightAngle = new Vector3(-35, 7, 0),
            },
            new LightParams()
            {
                TimeOfDay =TimeOfDay.Night,
                DirectionLightIntensity = 0.15f,
                DirectionLightAngle = new Vector3(-55, 13, 0),
            },
        };

        public LightParams GetLightParams( TimeOfDay timeOfDay)
        {
            return LightParams.FirstOrDefault(p => p.TimeOfDay == timeOfDay);
        }
    }

    [Serializable]
    public class LightParams
    {
        public TimeOfDay TimeOfDay;
        public float DirectionLightIntensity;
        public Vector3 DirectionLightAngle;
    }
}