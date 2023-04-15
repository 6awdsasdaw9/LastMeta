using UnityEngine;

namespace Code.Audio
{
    [CreateAssetMenu(fileName = "EnemyAudioPath", menuName = "ScriptableObjects/Audio/EnemyAudioPath")]
    public class EnemyAudioPath : ScriptableObject
    {
        public FMODUnity.EventReference breathPath;
        public FMODUnity.EventReference stepPath;
        public FMODUnity.EventReference attackStartPath;
        public FMODUnity.EventReference attackPath;
        public FMODUnity.EventReference attackEndPath;
        public FMODUnity.EventReference deathPath;
        public FMODUnity.EventReference screamPath;
        public FMODUnity.EventReference SFX;

    }
}