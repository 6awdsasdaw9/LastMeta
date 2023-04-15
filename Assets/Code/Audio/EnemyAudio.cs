using UnityEngine;
using FMOD;

namespace Code.Audio
{
    /// <summary>
    /// Animation Events
    /// </summary>
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private EnemyAudioPath _enemyAudioPath;

        private void AudioPlayBreath() => PlayAudio(_enemyAudioPath.breathPath.Path);

        private void AudioPlayStep() => PlayAudio(_enemyAudioPath.stepPath.Path);

        private void AudioPlayAttackStart() => PlayAudio(_enemyAudioPath.attackStartPath.Path);

        private void AudioPlayAttack() => PlayAudio(_enemyAudioPath.attackPath.Path);
        
        private void AudioPlayAttackEnd() => PlayAudio(_enemyAudioPath.attackEndPath.Path);

        private void AudioPlayDeath() => PlayAudio(_enemyAudioPath.deathPath.Path);

        private void AudioPlayScream() => PlayAudio(_enemyAudioPath.screamPath.Path);

        private void AudioPlaySFX() => PlayAudio(_enemyAudioPath.SFX.Path);
        
        private void PlayAudio(string path)
        {
            if(path == string.Empty)
                return;
            FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
        }
    }
}