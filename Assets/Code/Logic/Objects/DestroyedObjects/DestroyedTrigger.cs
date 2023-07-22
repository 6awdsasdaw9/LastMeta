using System.Collections.Generic;
using Code.Logic.Objects.Animations;
using Code.Logic.Triggers;
using Code.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Objects.DestroyedObjects
{
    public class DestroyedTrigger: MonoBehaviour, IEventSubscriber
    {
#if UNITY_EDITOR
        [ReadOnly, ShowInInspector] private bool _isSaved = false; 
        [Space]
#endif
        
        [SerializeField] private TriggerObserver _destructionTrigger;
        [SerializeField] private DestroyedAnimation _animation;
        [SerializeField] private List<MonoBehaviour> _disableComponents;
        [SerializeField] private List<GameObject> _disableObjects;

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
                _destructionTrigger.OnEnter += OnDestructionOnEnter;
            }
            else
            {
                _destructionTrigger.OnEnter -= OnDestructionOnEnter;
            }
        }

        private void OnDestructionOnEnter(Collider collider)
        {
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