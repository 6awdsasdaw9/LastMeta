using Code.Audio.AudioPath;
using FMODUnity;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private EnemyAudioPath _enemyAudioPath;

        #region Animation events
        private void AudioPlayBreath() => PlayAudio(_enemyAudioPath.BreathPath);
        private void AudioPlayStep() => PlayAudio(_enemyAudioPath.StepPath);
        private void AudioPlayMelleAttack() => PlayAudio(_enemyAudioPath.MelleAttackPath);
        private void AudioPlayRangeAttack() => PlayAudio(_enemyAudioPath.RangeAttackPath);
        private void AudioPlayDeath() => PlayAudio(_enemyAudioPath.DeathPath);
        private void AudioPlayScream() => PlayAudio(_enemyAudioPath.ScreamPath);
        private void AudioPlaySFX() => PlayAudio(_enemyAudioPath.SFX);
        
        #endregion
        
        private void PlayAudio(EventReference audioEvent)
        {
            if (audioEvent.IsNull) return;
            RuntimeManager.PlayOneShotAttached(audioEvent.Guid, gameObject);
        }
    }
}