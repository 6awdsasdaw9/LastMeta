using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameData/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [TitleGroup("Day settings", "", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true,
            indent: false)]
        public float DurationOfDayTime => DayTimeInSeconds * 0.5f;
        public float DayTimeInSeconds = 50;
        //public LightingSettings LightingSettings;
        
        [Space] 
        public PhysicsMaterials PhysicMaterial;
    }

    #region Physics

    [Serializable]
    public class PhysicsMaterials
    {
        public PhysicMaterial NoFrictionMaterial;
        public PhysicMaterial FrictionMaterial;
    }

    #endregion


    #region UISettings



        #endregion
}