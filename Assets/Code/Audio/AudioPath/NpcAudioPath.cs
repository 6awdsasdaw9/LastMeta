using FMODUnity;
using UnityEngine;

namespace Code.Audio.AudioPath
{
    [CreateAssetMenu(fileName = "NpcAudioPath", menuName = "ScriptableObjects/Audio/NpcAudioPath")]
    public class NpcAudioPath : ScriptableObject
    {
        public EventReference VoicePath;
        public EventReference StepPath;
        public EventReference SXF;
    }
}