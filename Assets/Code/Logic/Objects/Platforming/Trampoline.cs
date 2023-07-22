using Code.Audio.AudioEvents;
using Code.Logic.Collisions;
using Code.Logic.Objects.Animations;
using Code.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Objects.Platforming
{
    public class Trampoline : MonoBehaviour, IEventSubscriber
    {
        [SerializeField] private CollisionObserver _collision;
        [SerializeField] private Vector2 _force = Vector2.up;

        [Title("Optional")] 
        [SerializeField] private AudioEvent _audioEvent; 
        [SerializeField] private StartAnimation _startAnimation; 
        private void OnEnable()
        {
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _collision.OnEnter += OnEnter;
            }
            else
            {
                _collision.OnEnter -= OnEnter;
            }
        }

        private void OnEnter(Collision collision)
        {
            AddForce(collision);
            _audioEvent.PlayAudioEvent();
            _startAnimation?.PlayStart();
        }

        private void AddForce(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(_force, ForceMode.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, _force * 0.2f);
        }
    }
}