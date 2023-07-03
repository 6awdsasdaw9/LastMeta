using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace Code.Audio.AudioPath
{
    [CreateAssetMenu(fileName = "SceneAudioPath", menuName = "ScriptableObjects/Audio/SceneAudioPath")]
    public class SceneAudioPath : ScriptableObject
    {
        public List<SceneAudioData> SceneAudioData;
    }

    [Serializable]
    public class SceneAudioData
    {
        public Constants.Scenes Scene;
        public EventReference Music;
        public EventReference Ambience;
    }
    
}