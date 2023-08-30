using System;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Logic.Collisions
{
    public class CollisionParent : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private ColliderObserver _colliderObserver;

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
            if (flag)
            {
                _colliderObserver.OnEnter += OnEnter;
                _colliderObserver.OnExit += OnExit;
            }
            else
            {
                _colliderObserver.OnEnter -= OnEnter;
                _colliderObserver.OnExit -= OnExit;
            }
        }

        private void OnExit(GameObject obj)
        {
            obj.transform.SetParent(null);
        }

        private void OnEnter(GameObject obj)
        {
            obj.transform.SetParent(transform);
        }
    }
}