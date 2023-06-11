using FMODUnity;
using UnityEngine;

namespace Code.Audio.AudioPath
{
    [CreateAssetMenu(fileName = "EnemyAudioPath", menuName = "ScriptableObjects/Audio/EnemyAudioPath")]
    public class EnemyAudioPath : ScriptableObject
    {
        public EventReference breathPath;
        public EventReference stepPath;
        public EventReference attackStartPath;
        public EventReference attackPath;
        public EventReference attackEndPath;
        public EventReference deathPath;
        public EventReference screamPath;
        public EventReference SFX;
    }
}