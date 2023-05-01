using System;
using FMODUnity;
using UnityEngine;

public class AudioEventComponent : MonoBehaviour
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

        RuntimeManager.PlayOneShot(_eventReference, gameObject.transform.position);
    }
}

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
