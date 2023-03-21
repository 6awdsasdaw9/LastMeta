using UnityEngine;

namespace Code.Audio
{
    [CreateAssetMenu(fileName = "EnemyAudioPath", menuName = "ScriptableObjects/Audio/EnemyAudioPath")]
    public class EnemyAudioPath : ScriptableObject
    {
        public string breathPath;
        public string stepPath;
        public string attackStartPath;
        public string attackPath;
        public string attackEndPath;
        public string deathPath;
        
    }
}