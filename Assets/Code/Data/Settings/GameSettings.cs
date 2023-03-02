using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Day settings")]
        public float dayTimeInSeconds = 50;

        public float morningIntensity = 0.9f;
        public float eveningIntensity = 0.9f;
        public float nightIntensity = 0.2f;
        
        public Vector3 morningAngle = new Vector3(0,0.5f,0);
        public Vector3 eveningAngle = new Vector3(70,0.5f,0);
        public Vector3 nightAngle = new Vector3(70,0.5f,0);
    }
}