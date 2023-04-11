using Sirenix.OdinInspector;
using UnityEngine;
using LightingSettings = Code.Logic.DayOfTime.LightingSettings;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameData/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [TitleGroup("Day settings","", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        public float DayTimeInSeconds = 50; 
        public float DurationOfDayTime => DayTimeInSeconds / 3;

        [Space]
        public LightingSettings LightingSettings;

        [TitleGroup("UI Settings", "InteractiveObject", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        public float InteractiveCooldownTime = 1.1f;
        public Vector3 InteractiveObjectDownPos = Vector3.down * 1600;
        public Vector3 InteractiveObjectCenterPos = Vector3.zero;
        public float InteractiveObjectTimeToHide = 0.35f;
        public float InteractiveObjectTimeToShow = 0.5f;
        
    }
}