using UnityEngine;
using LightingSettings = Code.Logic.DayOfTime.LightingSettings;

namespace Code.Data.GameData
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Day settings")]
        public float dayTimeInSeconds = 50;
        public float durationOfDayTime => dayTimeInSeconds / 3;


        public LightingSettings lightingSettings;
        

    }
}