using UnityEngine;
using LightingSettings = Code.Logic.DayOfTime.LightingSettings;

namespace Code.Data.GameData
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/GameData/SettingsData")]
    public class SettingsData : ScriptableObject
    {
        [Header("Day settings")]
        public float dayTimeInSeconds = 50;
        public float durationOfDayTime => dayTimeInSeconds / 3;
        public float InteractiveCooldownTime = 0.7f;


        public LightingSettings lightingSettings;
        

    }
}