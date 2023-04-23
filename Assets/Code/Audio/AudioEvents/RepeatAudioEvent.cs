using System.Collections;

using UnityEngine;

namespace Code.Audio
{
    public class RepeatAudioEvent : MonoBehaviour
    {
        [SerializeField] private FMODUnity.EventReference _audioPath;
        [SerializeField] private float _repeatDelay = 5;
        [SerializeField] private float _delayBeforeStarting = 15;
        
        private bool _isPlaying;

        private void OnEnable()
        {
            PlayAudio();
        }

        private void OnDisable()
        {
            StopAudio();
        }

        private void PlayAudio()
        {
            /*if(_audioPath.Path == string.Empty)
                return;*/
            
            _isPlaying = true;
            StartCoroutine(RepeatCoroutine());
        }

        private void StopAudio()
        {
            /*if(_audioPath.Path == string.Empty)
                return;*/
            
            _isPlaying = false;
            StopAllCoroutines();
        }

        private IEnumerator RepeatCoroutine()
        {
            yield return new WaitForSeconds(_delayBeforeStarting);
            
            while (_isPlaying)
            {
                FMODUnity.RuntimeManager.PlayOneShot(_audioPath.Path);
                yield return new WaitForSeconds(_repeatDelay);
            }
        }
    }
}