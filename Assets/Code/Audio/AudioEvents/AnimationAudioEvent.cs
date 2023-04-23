using FMODUnity;
using UnityEngine;

public class AnimationAudioEvent : MonoBehaviour
{
    [SerializeField] private EventReference _eventReference;
 
    private void PlayAudioEvent()
    {
        if (_eventReference.IsNull)
        {
            return;
        }

        RuntimeManager.PlayOneShot(_eventReference, gameObject.transform.position);
    }
   
}
