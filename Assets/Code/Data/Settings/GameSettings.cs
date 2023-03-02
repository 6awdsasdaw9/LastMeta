using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Day settings")]
        public float dayTimeInSeconds = 50;

        public LightingSettings lightingSettings;
        

    }
}