using Code.Audio.AudioEvents;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Collisions;
using Code.Logic.Objects.Animations;
using Code.Services;
using Code.Services.EventsSubscribes;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Platforming
{
    public class Trampoline : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private ColliderObserver colliderObserver;
        [SerializeField] private Vector2 _force = Vector2.up;

        [Title("Optional")] 
        [GUIColor(0.85f, 0.74f, 1)]
        [SerializeField] private AudioEvent _audioEvent; 
        [SerializeField] private StartAnimation _startAnimation;

        private bool _isActive;
        
        [Inject]
        private void Constuct(EventsFacade eventsFacade)
        {
            eventsFacade.GameEvents.OnPause  += OnPause;
        }

        private void OnPause(bool isPause)
        {
            SubscribeToEvents(!isPause);
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        public void SubscribeToEvents(bool flag)
        {
            if(_isActive== flag)return;
            _isActive = flag;
            
            if (flag)
            {
                colliderObserver.OnEnter += OnEnter;
            }
            else
            {
                colliderObserver.OnEnter -= OnEnter;
            }
        }

        private void OnEnter(GameObject collision)
        {
            AddForce(collision);
            _audioEvent.PlayAudioEvent();
            _startAnimation?.PlayStart();
        }

        private void AddForce(GameObject collision)
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