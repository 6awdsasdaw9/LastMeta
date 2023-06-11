using System;
using FMODUnity;
using UnityEngine;

namespace Code.Audio.AudioEvents
{
    [Serializable]
    public class AudioEvent
    {
        [SerializeField] private EventReference _eventReference;
        [SerializeField] private bool _playOnAwake;

        private void OnEnable()
        {
            if (_playOnAwake)
            {
                PlayAudioEvent();
            }
        }

        public void PlayAudioEvent()
        {
            if (_eventReference.IsNull)
            {
                return;
            }

            RuntimeManager.PlayOneShot(_eventReference);
        }
    }
}