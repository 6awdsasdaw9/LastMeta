using System;
using System.Collections.Generic;
using Code.Logic.Objects;
using Code.Logic.Triggers;
using Code.Services;
using UnityEngine;

namespace Code.Character.Common
{
    public class SelfDestructionObject: MonoBehaviour, IEventSubscriber
    {
        [SerializeField] private TriggerObserver _destructionTrigger;
        [SerializeField] private DestroyObjectAnimation _animation;
        [SerializeField] private List<MonoBehaviour> _disableComponents;
        [SerializeField] private List<GameObject> _disableObjects;
        
        public Action OnSelfDestruction;

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
                _destructionTrigger.TriggerEnter += OnDestructionTriggerEnter;
            }
            else
            {
                
                _destructionTrigger.TriggerEnter -= OnDestructionTriggerEnter;
            }
        }

        private void OnDestructionTriggerEnter(Collider collider)
        {
            OnSelfDestruction?.Invoke();
            _animation.PlayDestroy();
            foreach (var component in _disableComponents)
            {
                component.enabled = false;
            }
            foreach (var obj in _disableObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}