using System;
using UnityEngine;

namespace Code.Logic.DayOfTime
{
    [Serializable]
    public class LightingSettings
    {
        public float morningIntensity = 0.9f;
        public float eveningIntensity = 0.9f;
        public float nightIntensity = 0.2f;
        
        public Vector3 morningAngle = new Vector3(0,0.5f,0);
        public Vector3 eveningAngle = new Vector3(70,0.5f,0);
        public Vector3 nightAngle = new Vector3(70,0.5f,0);
    }
}