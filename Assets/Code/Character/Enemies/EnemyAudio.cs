using UnityEngine;
using FMOD;
using FMODUnity;
using Debug = FMOD.Debug;

namespace Code.Audio
{
    /// <summary>
    /// Animation Events
    /// </summary>
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private EnemyAudioPath _enemyAudioPath;

        private void AudioPlayBreath()
        {
            PlayAudio(_enemyAudioPath.breathPath);
        }

        private void AudioPlayStep()
        {
            PlayAudio(_enemyAudioPath.stepPath);
        }

        private void AudioPlayAttackStart() => PlayAudio(_enemyAudioPath.attackStartPath);

        private void AudioPlayAttack() => PlayAudio(_enemyAudioPath.attackPath);

        private void AudioPlayAttackEnd() => PlayAudio(_enemyAudioPath.attackEndPath);

        private void AudioPlayDeath() => PlayAudio(_enemyAudioPath.deathPath);

        private void AudioPlayScream() => PlayAudio(_enemyAudioPath.screamPath);

        private void AudioPlaySFX() => PlayAudio(_enemyAudioPath.SFX);


        private void PlayAudio(EventReference audioEvent)
        {
            if (audioEvent.IsNull)
            {
                return;
            }

            RuntimeManager.PlayOneShotAttached(audioEvent.Guid, gameObject);
        }
    }
}