using System.Collections.Generic;
using Code.Logic.Objects.Animations;
using Code.Logic.Triggers;
using Code.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.DestroyedObjects
{
    public class DestroyedTrigger: MonoBehaviour, IEventsSubscriber
    {
        [InfoBox("Is not saved data")]
        [SerializeField] private TriggerObserver _destructionTrigger;
        [SerializeField] private DestroyedAnimation _animation;
        [SerializeField] private List<MonoBehaviour> _disableComponents;
        [SerializeField] private List<GameObject> _disableObjects;

        [Inject]
        private void Construct(EventSubsribersStorage eventSubsribersStorage)
        {
            eventSubsribersStorage.Add(this);
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