using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Audio.AudioEvents
{
    public class AudioRaycastEvent : MonoBehaviour
    {
        [SerializeField] private AudioEvent _audioEvent;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Vector2 _startRaycastPoint;
        [SerializeField] private float _distance;

        private void Start()
        {
            StartCheck().Forget();
        }

        private async UniTaskVoid StartCheck()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1),cancellationToken: this.GetCancellationTokenOnDestroy());
            if (RaycastIsTrue())
            {
                enabled = false;
            }
            else
            {
                WaitRaycast().Forget();
            }
        }

        private async UniTaskVoid WaitRaycast()
        {
            await UniTask.WaitUntil(RaycastIsTrue, cancellationToken: this.GetCancellationTokenOnDestroy());
            _audioEvent.PlayAudioEvent();
        }

        private bool RaycastIsTrue()
        {
            return Physics.Raycast(transform.position + (Vector3)_startRaycastPoint, Vector3.up, _distance, _layerMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position + (Vector3)_startRaycastPoint, Vector3.up * _distance);
        }
    }
}