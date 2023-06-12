using System;
using System.Collections.Generic;
using System.Linq;
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
        [GUIColor(0,0.5f,0.5f,0.8f)]
        public Constants.Scenes Scene;
        [EnumToggleButtons] 
        public Constants.GameMode GameMode;

        [Title("Music")] 
        public EventReference Music;
        public EventReference Ambience;

        [Title("Lighting")] 
        public TimeOfDaySettings TimeOfDaySettings;
    }

}