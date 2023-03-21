using UnityEngine;

namespace Code.Audio
{
    /// <summary>
    /// Animation Events
    /// </summary>
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private EnemyAudioPath _enemyAudioPath;
        
        private void AudioPlayBreath() => 
            FMODUnity.RuntimeManager.PlayOneShotAttached(_enemyAudioPath.breathPath, gameObject);

        private void AudioPlayStep() => 
            FMODUnity.RuntimeManager.PlayOneShotAttached(_enemyAudioPath.stepPath, gameObject);
        
        private void AudioPlayAttackStart() => 
            FMODUnity.RuntimeManager.PlayOneShotAttached(_enemyAudioPath.attackStartPath, gameObject);
        
        private void AudioPlayAttack() => 
            FMODUnity.RuntimeManager.PlayOneShotAttached(_enemyAudioPath.attackPath, gameObject);
        
        private void AudioPlayAttackEnd() => 
            FMODUnity.RuntimeManager.PlayOneShotAttached(_enemyAudioPath.attackEndPath, gameObject);
        
        private void AudioPlayDeath() => 
            FMODUnity.RuntimeManager.PlayOneShotAttached(_enemyAudioPath.deathPath, gameObject);
    }
}